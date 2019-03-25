
using System;
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
    public class NationBalanceData : StaticDataReferenceBalanceData<NationalityDataSource, Nationality, NationBalanceObject, Nationality, NationBalanceObject>
    {
        public const string NATION_DATA_PATH = "Assets/AssetBundles/Data/Nations/";
        
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