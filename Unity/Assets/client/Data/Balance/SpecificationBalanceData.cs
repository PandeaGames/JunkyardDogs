
using System;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using JunkyardDogs.Specifications;
using UnityEngine;
using Material = JunkyardDogs.Specifications.Material;

[Serializable]
public struct SpecificationBalanceObject:IStaticDataBalanceObject
{
    public string name;
    public string GetDataUID()
    {
        return name;
    }
}
[CreateAssetMenu]
public class SpecificationBalanceData : StaticDataReferenceBalanceData<SpecificationDataSource, Specification, SpecificationBalanceObject>
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
