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
    
    public abstract class StaticDataReferenceBalanceData<TStaticDataList, TUnityData, TBalanceObject> : BalanceData 
        where TStaticDataList:AbstractScriptableObjectStaticData<TUnityData> 
        where TUnityData:ScriptableObject, IStaticDataBalance<TBalanceObject> 
        where TBalanceObject:IStaticDataBalanceObject
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
            
            foreach (TBalanceObject nationBalanceObj in balanceObjects)
            {
                TUnityData nationality = FindData(nationBalanceObj, _dataList.Data);

                if (nationality == null)
                {
                    nationality = ScriptableObject.CreateInstance<TUnityData>();
                    AssetDatabase.CreateAsset(nationality, GetNewDataFolder() + nationBalanceObj.GetDataUID()+".asset");
                    _dataList.Data.Add(nationality);
                }

                nationality.ApplyBalance(nationBalanceObj);
            }
            
            EditorUtility.SetDirty(_dataList);
            AssetDatabase.SaveAssets();
        }
        
        protected TUnityData FindData(TBalanceObject balance, List<TUnityData> list)
        {
            foreach (TUnityData data in list)
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

            foreach (TUnityData unityData in _dataList.Data)
            {
                TBalanceObject data = unityData.GetBalance();
                rowData.Add(new RowData(data.GetDataUID(), JsonUtility.ToJson(data)));
            }

            return rowData.ToArray();
        }
    }
}
#endif