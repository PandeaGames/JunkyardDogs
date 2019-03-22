#if UNITY_EDITOR
using System.Collections.Generic;
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
    
    public abstract class StaticDataReferenceBalanceData<TStaticDataList, TUnityData, TBalanceObject> : BalanceData where TStaticDataList:AbstractScriptableObjectStaticData<TUnityData> where TUnityData:ScriptableObject, IStaticDataBalance<TBalanceObject> where TBalanceObject:IStaticDataBalanceObject
    {
        [SerializeField]
        protected TStaticDataList _dataList;

        protected abstract string GetNewDataFolder();
        
        
        public override void ImportData(string json)
        {
            // Parse from json to the desired object type.            
            TBalanceObject[] nations = JsonHelper.ArrayFromJson<TBalanceObject>(json);

            Debug.LogFormat("{0} Nation found. Parsing.", nations.Length);
            
            foreach (TBalanceObject nationBalanceObj in nations)
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
        
        private TUnityData FindData(TBalanceObject balance, List<TUnityData> list)
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

            foreach (TUnityData nationality in _dataList.Data)
            {
                TBalanceObject data = nationality.GetBalance();
                rowData.Add(new RowData(data.GetDataUID(), JsonUtility.ToJson(data)));
            }

            return rowData.ToArray();
        }
    }
}
#endif