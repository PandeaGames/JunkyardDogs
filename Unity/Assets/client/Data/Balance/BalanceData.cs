
using GoogleSheetsForUnity;
using UnityEngine;

namespace JunkyardDogs.Data.Balance
{
    [CreateAssetMenu(menuName = MENU_NAME)]
    public abstract class BalanceData: BalanceDataBase
    {
        private const string MENU_NAME =  BalanceDataUtilites.BALANCE_MENU_FOLDER + "Balance Data";
        
        public const string ListDelimiter = ",";
        public const char ListDelimiterChar = ',';
        public const char DataDelimiterChar = ':';
        public const string DataDelimiter = ":";
        
        protected TBalanceObject[] Parse<TBalanceObject>(string json)
     {
         TBalanceObject[] data = JsonHelper.ArrayFromJson<TBalanceObject>(json);
         return data;
     }

        public abstract void ImportData(string json);

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
    
        public abstract RowData[] GetData();
        public abstract string[] GetFieldNames();
        public abstract string GetUIDFieldName();
    }
}
