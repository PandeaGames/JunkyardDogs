#if UNITY_EDITOR
using System.Collections.Generic;
using System.Reflection;
using GoogleSheetsForUnity;
using PandeaGames.Data.Static;
using UnityEditor;
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
    
    public abstract class StaticDataReferenceBalanceData<TStaticDataList, TUnityDataBase, TBalanceObjectBase, TUnityData,TBalanceObject>
        :BalanceData
        where TStaticDataList : AbstractScriptableObjectStaticData<TUnityDataBase>
        where TUnityData : ScriptableObject, IStaticDataBalance<TBalanceObject>, TUnityDataBase
        where TUnityDataBase : ScriptableObject
        where TBalanceObject : IStaticDataBalanceObject
        where TBalanceObjectBase : IStaticDataBalanceObject
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
            // Parse from json to the desired object type.            
            TBalanceObject[] balanceObjects = JsonHelper.ArrayFromJson<TBalanceObject>(json);

            Debug.LogFormat("{0} Data found. Parsing.", balanceObjects.Length);
            
            foreach (TBalanceObject balanceObj in balanceObjects)
            {   
                TUnityData data = FindData(balanceObj, _dataList.Data);

                if (data == null && AllowDataCreationOnImport)
                {
                    data = ScriptableObject.CreateInstance<TUnityData>();
                    AssetDatabase.CreateAsset(data, GetNewDataFolder() + balanceObj.GetDataUID()+".asset");
                    _dataList.Data.Add(data);
                }

                if (data)
                {
                    data.ApplyBalance(balanceObj);
                }
            }
            
            EditorUtility.SetDirty(_dataList);
            AssetDatabase.SaveAssets();
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
        
        protected TUnityDataBase FindData(TBalanceObjectBase balance, List<TUnityDataBase> list)
        {
            foreach (TUnityDataBase data in list)
            {
                if (data != null && data.name == balance.GetDataUID())
                {
                    return data;
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
#endif