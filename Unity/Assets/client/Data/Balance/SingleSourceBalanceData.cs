using System.Reflection;
using GoogleSheetsForUnity;
using UnityEngine;

namespace JunkyardDogs.Data.Balance
{
    public class SingleSourceBalanceData<TData, TBalanceObject> : BalanceData where TData : ScriptableObject, IStaticDataBalance<TBalanceObject> where TBalanceObject:IStaticDataBalanceObject, new ()
    {
        [SerializeField]
        private TData[] _data;
        
        public override void ImportData(string json)
        {
            #if UNITY_EDITOR
            // Parse from json to the desired object type.            
            TBalanceObject[] balanceObjects = JsonHelper.ArrayFromJson<TBalanceObject>(json);

            for (int i = 0; i < balanceObjects.Length; i++)
            {
                if (_data.Length > i)
                {
                    _data[i].ApplyBalance(balanceObjects[i]);
                }
            }
            
            #endif
        }

        public override RowData[] GetData()
        {
            throw new System.NotImplementedException();
        }

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

        public override string GetUIDFieldName()
        {
            return new TBalanceObject().GetDataUID();
        }
    }
}