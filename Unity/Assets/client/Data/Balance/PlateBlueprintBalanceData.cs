using System;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using UnityEngine;

[Serializable]
public struct PlateBlueprintBalanceObject:IStaticDataBalanceObject
{
    public string name;
    public string specification;
    public string manufacturer;
    
    public string GetDataUID()
    {
        return name;
    }
}

[CreateAssetMenu]
public class PlateBlueprintBalanceData : StaticDataReferenceBalanceData<
    BlueprintDataSource, 
    BlueprintDataBase, 
    BlueprintBalanceObject,
    PlateBlueprintData, 
    PlateBlueprintBalanceObject>
{
    public const string DATA_PATH = "Assets/AssetBundles/Data/Blueprints/Components/Plates/";
    
    public override string GetUIDFieldName()
    {
        return "name";
    }

    protected override string GetNewDataFolder()
    {
        return DATA_PATH;
    }
}
