using System;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using JunkyardDogs.Specifications;
using UnityEngine;

[Serializable]
public class PhysicalSpecificationBalanceObject:SpecificationBalanceObject
{
    public float volume;
}

public class PhysicalSpecificationBalanceData : StaticDataReferenceBalanceData<
    SpecificationDataSource, 
    Specification,
    PhysicalSpecification,
    PhysicalSpecificationBalanceObject>
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
