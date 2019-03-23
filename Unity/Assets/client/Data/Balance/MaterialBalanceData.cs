
using System;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using UnityEngine;
using Material = JunkyardDogs.Specifications.Material;

[Serializable]
public struct MaterialBalanceObject:IStaticDataBalanceObject
{
    public string name;
    public double density;
    public string GetDataUID()
    {
        return name;
    }
}
[CreateAssetMenu]
public class MaterialBalanceData : StaticDataReferenceBalanceData<MaterialDataSource, Material, MaterialBalanceObject, Material, MaterialBalanceObject>
{
    public const string DATA_PATH = "Assets/AssetBundles/Data/Products/Materials/";
    
    protected override string GetNewDataFolder()
    {
        return DATA_PATH;
    }

    public override string GetUIDFieldName()
    {
        return "name";
    }
}
