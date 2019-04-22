
using System;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using JunkyardDogs.Specifications;
using UnityEngine;

[Serializable]
public class MeleeWeaponBalanceObject:PhysicalSpecificationBalanceObject
{
    public double cooldown;
    public double chargeTime;
    public int range;
    public int damage;
}

[CreateAssetMenu(menuName = MENU_NAME)]
public class MeleeWeaponBalanceData : StaticDataReferenceBalanceData<
    SpecificationDataSource, 
    Specification,
    Melee,
    MeleeWeaponBalanceObject>
{
    private const string MENU_NAME =  BalanceDataUtilites.BALANCE_MENU_FOLDER + "MeleeWeaponBalanceData";

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
