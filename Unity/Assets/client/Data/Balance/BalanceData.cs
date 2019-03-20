#if UNITY_EDITOR
using GoogleSheetsForUnity;
using UnityEngine;

namespace JunkyardDogs.Data.Balance
{
    [CreateAssetMenu]
    public abstract class BalanceData: ScriptableObject
    {
        public struct RowData
        {
            public string UID;
            public string Json;

            public RowData(string UID, string Json)
            {
                this.UID = UID;
                this.Json = Json;
            }
        }
        
        [SerializeField]
        public string TableName;
     //PlayerInfo[] players = JsonHelper.ArrayFromJson<PlayerInfo>(rawJSon);
     protected TBalanceObject[] Parse<TBalanceObject>(string json)
     {
         TBalanceObject[] data = JsonHelper.ArrayFromJson<TBalanceObject>(json);
         return data;
     }

     public abstract void ImportData(string json);
     public abstract RowData[] GetData();
     public abstract string[] GetFieldNames();
     public abstract string GetUIDFieldName();

    }
}
#endif