using System;
using JunkyardDogs.Specifications;
using UnityEngine;

namespace JunkyardDogs.Data.Balance
{
    [Serializable]
    public struct PlateBalanceObject:IStaticDataBalanceObject
    {
        public string name;
        public float volume;
        public float thickness;
        public string GetDataUID()
        {
            return name;
        }
    }
    
    [CreateAssetMenu]
    public class PlateBalanceData : StaticDataReferenceBalanceData<SpecificationDataSource, Specification,
        SpecificationBalanceObject, Plate, PlateBalanceObject>
    {
        public const string DATA_PATH = "Assets/AssetBundles/Data/Products/Plating/";

        protected override string GetNewDataFolder()
        {
            return DATA_PATH;
        }

        public override string GetUIDFieldName()
        {
            return "name";
        }
    }
}