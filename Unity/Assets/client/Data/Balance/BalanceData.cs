
using GoogleSheetsForUnity;
using UnityEngine;

namespace JunkyardDogs.Data.Balance
{
    [CreateAssetMenu]
    public abstract class BalanceData: ScriptableObject
    {
        public const string ListDelimiter = ",";
        public const char ListDelimiterChar = ',';
        public const char DataDelimiterChar = ':';
        public const string DataDelimiter = ":";
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

        [SerializeField]
        public bool AllowImport;
        
        [SerializeField]
        public bool AllowDataCreationOnImport;

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
