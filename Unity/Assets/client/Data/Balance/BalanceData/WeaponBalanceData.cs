
using System;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using JunkyardDogs.Specifications;
using UnityEngine;

[Serializable]
public class WeaponBalanceObject:SpecificationBalanceObject
{
    public double cooldown;
    public double chargeTime;
}

[CreateAssetMenu]
public class WeaponBalanceData : StaticDataReferenceBalanceData<
    SpecificationDataSource, 
    Specification, 
    BlueprintBalanceObject,
    Weapon, 
    WeaponBalanceObject>

{
    public const string NEW_DATA_PATH = "Assets/AssetBundles/Data/Products/Weapons/";
    
    public override string GetUIDFieldName()
    {
        return "name";
    }

    protected override string GetNewDataFolder()
    {
        return NEW_DATA_PATH;
    }
}
