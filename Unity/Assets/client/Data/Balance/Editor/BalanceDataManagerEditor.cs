using GoogleSheetsForUnity;
using UnityEditor;
using System;
using UnityEngine;

namespace JunkyardDogs.Data.Balance.Editor
{
    [CustomEditor(typeof(BalanceManagerData))]
    public class BalanceDataManagerEditor : UnityEditor.Editor
    {
        private bool isImporting;
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUI.BeginDisabledGroup(isImporting);
            
            if (GUILayout.Button("Import All"))
            {
                OnImport();
            }
            
            if (GUILayout.Button("Export All"))
            {
                OnExport();
            }
            
            if (GUILayout.Button("Create All"))
            {
                OnCreate();
            }
            
            foreach (BalanceData balanceData in (target as BalanceManagerData).BalanceData)
            {
                if (balanceData == null)
                {
                    continue;
                }    
                    
                EditorGUILayout.LabelField(balanceData.TableName);
                EditorGUI.BeginDisabledGroup(!balanceData.AllowImport);
                if (GUILayout.Button("Import"))
                {
                    OnImport(balanceData);
                }
                EditorGUI.EndDisabledGroup();
            
                if (GUILayout.Button("Export"))
                {
                    OnExport(balanceData);
                }
            
                if (GUILayout.Button("Create"))
                {
                    OnCreate(balanceData);
                }
            }
            
            EditorGUI.EndDisabledGroup();
        }
        
        private void OnEnable()
        {
            // Subscribe for catching cloud responses.
            Drive.responseCallback += HandleDriveResponse;
        }

        private void OnDisable()
        {
            // Remove listeners.
            Drive.responseCallback -= HandleDriveResponse;
        }

        private void OnImport(BalanceData balanceData)
        {
            Drive.GetTable(balanceData.TableName, false);
            isImporting = true;
            EditorUtility.DisplayProgressBar("Importing", "Importing '" +balanceData.name+"' tables from Google Drive",0f);

        }

        private void OnImport()
        {
            // Get all objects from table 'PlayerInfo'.
            Drive.GetAllTables(false);
            isImporting = true;
            EditorUtility.DisplayProgressBar("Importing", "Importing all tables from Google Drive",0f);
        }

        private void OnExport(BalanceData balanceData)
        {
            foreach (BalanceData.RowData rowData in balanceData.GetData())
            {
                Drive.UpdateObjects(balanceData.TableName, balanceData.GetUIDFieldName(), rowData.UID, rowData.Json, true, false);
            }
        }

        private void OnExport()
        {
            //Drive.UpdateObjects(_tableName, "name", _playerData.name, jsonPlayer, create, true);
            (target as BalanceManagerData).BalanceData.ForEach(balanceData =>
            {
                OnExport(balanceData);
            });
        }

        public void OnCreate(BalanceData balanceData)
        {
            // Request for the table to be created on the cloud.
            Drive.CreateTable(balanceData.GetFieldNames(), balanceData.TableName, false);
        }

        public void OnCreate()
        {
            (target as BalanceManagerData).BalanceData.ForEach(balanceData => { OnCreate(balanceData); });
        }

        public void HandleDriveResponse(Drive.DataContainer dataContainer)
        {
            UnityEngine.Debug.Log(dataContainer);
            
            // First check the type of answer.
            if (dataContainer.QueryType == Drive.QueryType.getTable)
            {
                EditorUtility.DisplayProgressBar("Importing", "Applying data",0.5f);
                string rawJSon = dataContainer.payload;
                Debug.Log(rawJSon);

                foreach (BalanceData balanceData in (target as BalanceManagerData).BalanceData)
                {
                    try
                    {
                        // Check if the type is correct.
                        if (string.Compare(dataContainer.objType, balanceData.TableName) == 0)
                        {
                            balanceData.ImportData(rawJSon);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogErrorFormat("There was an error applying balance against {0}:\n", balanceData.name);
                        break;
                    }
                }
                
                AssetDatabase.SaveAssets();
                EditorUtility.ClearProgressBar();
                EditorUtility.DisplayDialog("Complete", "Import Complete", "OK");
                isImporting = false;
            }
            
            // First check the type of answer.
            if (dataContainer.QueryType == Drive.QueryType.getAllTables)
            {
                EditorUtility.DisplayProgressBar("Importing", "Applying data",0.5f);
                string rawJSon = dataContainer.payload;

                // The response for this query is a json list of objects that hold tow fields:
                // * objType: the table name (we use for identifying the type).
                // * payload: the contents of the table in json format.
                Drive.DataContainer[] tables = JsonHelper.ArrayFromJson<Drive.DataContainer>(rawJSon);

                foreach (Drive.DataContainer tableDataContainer in tables)
                {
                    foreach (BalanceData balanceData in (target as BalanceManagerData).BalanceData)
                    {
                        // Check if the type is correct.
                        if (string.Compare(tableDataContainer.objType, balanceData.TableName) == 0)
                        {
                            balanceData.ImportData(tableDataContainer.payload);
                        }
                    }
                }
                
                AssetDatabase.SaveAssets();
                EditorUtility.ClearProgressBar();
                EditorUtility.DisplayDialog("Complete", "Import Complete", "OK");
                isImporting = false;
            }
        }
    }
}