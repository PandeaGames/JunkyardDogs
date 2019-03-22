#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using GoogleSheetsForUnity;
using UnityEditor;
using UnityEngine;

namespace JunkyardDogs.Data.Balance
{
    [Serializable]
    public struct NationBalanceObject:IStaticDataBalanceObject
    {
        public string name;
        public string GetDataUID()
        {
            return name;
        }
    }
    
    [CreateAssetMenu]
    public class NationBalanceData : StaticDataReferenceBalanceData<NationalityDataSource, Nationality, NationBalanceObject>
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

        protected override string GetNewDataFolder()
        {
            return NATION_DATA_PATH;
        }
    }
}
#endif