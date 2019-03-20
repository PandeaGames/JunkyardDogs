#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using GoogleSheetsForUnity;
using UnityEditor;
using UnityEngine;

namespace JunkyardDogs.Data.Balance
{
    [Serializable]
    public struct NationBalanceObject
    {
        public string name;
    }
    
    [CreateAssetMenu]
    public class NationBalanceData : StaticDataReferenceBalanceData<NationalityDataSource, Nationality>
    {
        public const string NATION_DATA_PATH = "Assets/AssetBundles/Data/Nations/";

        public override string[] GetFieldNames()
        {
            string[] fieldNames = new string[1];
            fieldNames[0] = "name";

            return fieldNames;
        }
        
        public override string GetUIDFieldName()
        {
            return "name";
        }

        public override void ImportData(string json)
        {
            // Parse from json to the desired object type.            
            NationBalanceObject[] nations = JsonHelper.ArrayFromJson<NationBalanceObject>(json);

            Debug.LogFormat("{0} Nation found. Parsing.", nations.Length);
            
            foreach (NationBalanceObject nationBalanceObj in nations)
            {
                Nationality nationality = FindNationality(nationBalanceObj, _dataList.Data);

                if (nationality == null)
                {
                    nationality = ScriptableObject.CreateInstance<Nationality>();
                    AssetDatabase.CreateAsset(nationality, NATION_DATA_PATH+nationBalanceObj.name+".asset");
                    _dataList.Data.Add(nationality);
                }
                
                nationality.ApplyBalance(nationBalanceObj);
            }
            
            EditorUtility.SetDirty(_dataList);
            AssetDatabase.SaveAssets();
        }

        private Nationality FindNationality(NationBalanceObject balance, List<Nationality> list)
        {
            foreach (Nationality nationality in list)
            {
                if (nationality != null && nationality.name == balance.name)
                {
                    return nationality;
                }
            }

            return null;
        }

        public override RowData[] GetData()
        {
            List<RowData> rowData = new List<RowData>();

            foreach (Nationality nationality in _dataList.Data)
            {
                NationBalanceObject data = new NationBalanceObject();
                data.name = nationality.name;
                rowData.Add(new RowData(data.name, JsonUtility.ToJson(data)));
            }

            return rowData.ToArray();
        }
    }
}
#endif