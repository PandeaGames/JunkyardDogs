using System;
using JunkyardDogs.Specifications;
using UnityEngine;

namespace JunkyardDogs.Data.Balance
{
    [Serializable]
    public struct PhysicalSpecificationBalanceObject:IStaticDataBalanceObject
    {
        public string name;
        public float volume;
        public string GetDataUID()
        {
            return name;
        }
    }
    
    [CreateAssetMenu]
    public class PhysicalSpecificationBalanceData : StaticDataReferenceBalanceData<SpecificationDataSource, Specification,
        SpecificationBalanceObject, PhysicalSpecification, PhysicalSpecificationBalanceObject>
    {
        public const string DATA_PATH = "Assets/AssetBundles/Data/Products/";

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