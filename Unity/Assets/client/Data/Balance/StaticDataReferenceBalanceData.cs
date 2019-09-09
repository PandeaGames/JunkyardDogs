
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using GoogleSheetsForUnity;
using PandeaGames.Data.Static;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace JunkyardDogs.Data.Balance
{
    public interface IStaticDataBalance<TBalanceObject>
    {
        void ApplyBalance(TBalanceObject balance);
        TBalanceObject GetBalance();
    }

    public interface IStaticDataBalanceObject
    {
        string GetDataUID();
    }
    
    public abstract class StaticDataReferenceBalanceData<TStaticDataList, TUnityDataBase, TUnityData, TBalanceObject>
        :BalanceData
        where TStaticDataList : AbstractScriptableObjectStaticData<TUnityDataBase>
        where TUnityData : ScriptableObject, IStaticDataBalance<TBalanceObject>, TUnityDataBase
        where TUnityDataBase : ScriptableObject
        where TBalanceObject : IStaticDataBalanceObject
    {
        [SerializeField]
        protected TStaticDataList _dataList;

        protected abstract string GetNewDataFolder();

        public override string[] GetFieldNames()
        {
            FieldInfo[] fields = typeof(TBalanceObject).GetFields(BindingFlags.Public | BindingFlags.Instance);
            
            string[] fieldNames = new string[fields.Length];

            for (int i = 0; i < fields.Length; i++)
            {
                fieldNames[i] = fields[i].Name;
            }

            return fieldNames;
        }

        public override void ImportData(string json)
        {
#if UNITY_EDITOR
            // Parse from json to the desired object type.            
            TBalanceObject[] balanceObjects = JsonHelper.ArrayFromJson<TBalanceObject>(json);

            Debug.LogFormat("{0} Data found. Parsing.", balanceObjects.Length);
            
            foreach (TBalanceObject balanceObj in balanceObjects)
            {   
                TUnityData data = FindData(balanceObj, _dataList.Data);

                if (data == null && AllowDataCreationOnImport)
                {

                    data = ScriptableObject.CreateInstance<TUnityData>();

                    if (!Directory.Exists(GetNewDataFolder()))
                    {
                        Directory.CreateDirectory(GetNewDataFolder());
                        AssetDatabase.Refresh();
                        Debug.LogError("Directory Does not Exist: "+GetNewDataFolder());
                    }
                    
                    AssetDatabase.CreateAsset(data, GetNewDataFolder() + balanceObj.GetDataUID()+".asset");
                    _dataList.Data.Add(data);
                    
                }

                if (data)
                {
                    data.ApplyBalance(balanceObj);
                    EditorUtility.SetDirty(data);
                }
            }
            
            EditorUtility.SetDirty(_dataList);
            AssetDatabase.SaveAssets();
#endif
        }
        
        protected TUnityData FindData(TBalanceObject balance, List<TUnityDataBase> list)
        {
            foreach (TUnityDataBase data in list)
            {
                if (data != null && data.name == balance.GetDataUID() && data is TUnityData)
                {
                    return (TUnityData) data;
                }
            }

            return null;
        }

        public override RowData[] GetData()
        {
            List<RowData> rowData = new List<RowData>();

            foreach (TUnityDataBase unityDataBase in _dataList.Data)
            {
                if (unityDataBase is TUnityData)
                {
                    TUnityData unityData = (TUnityData) unityDataBase;
                    TBalanceObject data = unityData.GetBalance();
                    rowData.Add(new RowData(data.GetDataUID(), JsonUtility.ToJson(data)));
                }
            }

            return rowData.ToArray();
        }
    }
}
