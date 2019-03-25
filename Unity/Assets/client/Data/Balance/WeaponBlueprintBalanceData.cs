using System;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using UnityEngine;

[Serializable]
public struct WeaponBlueprintBalanceObject:IStaticDataBalanceObject
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
public class WeaponBlueprintBalanceData : StaticDataReferenceBalanceData<
    BlueprintDataSource, 
    BlueprintDataBase, 
    BlueprintBalanceObject,
    WeaponBlueprintData, 
    WeaponBlueprintBalanceObject>
{
    public const string DATA_PATH = "Assets/AssetBundles/Data/Blueprints/Components/Weapons/";
    
    public override string GetUIDFieldName()
    {
        return "name";
    }

    protected override string GetNewDataFolder()
    {
        return DATA_PATH;
    }
}
