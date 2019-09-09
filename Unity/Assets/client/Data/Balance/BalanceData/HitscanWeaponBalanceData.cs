
using System;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using JunkyardDogs.Specifications;
using UnityEngine;

[Serializable]
public class HitscanWeaponBalanceObject:PhysicalSpecificationBalanceObject
{
    public double cooldown;
    public double chargeTime;
    public int damage;
    public int armourPiercing;
}

[CreateAssetMenu(menuName = MENU_NAME)]
public class HitscanWeaponBalanceData : StaticDataReferenceBalanceData<
    SpecificationDataSource, 
    Specification,
    Hitscan,
    HitscanWeaponBalanceObject>
{
    private const string MENU_NAME =  BalanceDataUtilites.BALANCE_MENU_FOLDER + "HitscanWeaponBalanceData";

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
