using System;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using UnityEngine;

[Serializable]
public struct WeaponProcessorBlueprintBalanceObject:IStaticDataBalanceObject
{
    public string name;
    public string specification;
    public string manufacturer;
    public string weapon;
    public string GetDataUID()
    {
        return name;
    }
}

[CreateAssetMenu]
public class WeaponProcessorBlueprintBalanceData : StaticDataReferenceBalanceData<
    BlueprintDataSource, 
    BlueprintDataBase, 
    BlueprintBalanceObject,
    WeaponProcessorBlueprintData, 
    WeaponProcessorBlueprintBalanceObject>
{
    public const string DATA_PATH = "Assets/AssetBundles/Data/Blueprints/Components/WeaponProcessors/";
    
    public override string GetUIDFieldName()
    {
        return "name";
    }

    protected override string GetNewDataFolder()
    {
        return DATA_PATH;
    }
}
