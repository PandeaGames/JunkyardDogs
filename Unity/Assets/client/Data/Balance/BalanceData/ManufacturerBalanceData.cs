
using System;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using JunkyardDogs.Specifications;
using UnityEngine;
using Material = JunkyardDogs.Specifications.Material;

[Serializable]
public struct ManufacturerBalanceObject:IStaticDataBalanceObject
{
    public string name;
    public string nationality;
    public string GetDataUID()
    {
        return name;
    }
}
[CreateAssetMenu]
public class ManufacturerBalanceData : StaticDataReferenceBalanceData<ManufactuererDataSource, Manufacturer, ManufacturerBalanceObject, Manufacturer, ManufacturerBalanceObject>
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
