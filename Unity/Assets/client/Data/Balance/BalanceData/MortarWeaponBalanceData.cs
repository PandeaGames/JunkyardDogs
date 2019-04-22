
using System;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using JunkyardDogs.Specifications;
using UnityEngine;

[Serializable]
public class MortarWeaponBalanceObject:ProjectileWeaponBalanceObject
{
    public double cooldown;
    public double chargeTime;
    public int range;
    public int damage;
    public int radius;
}

[CreateAssetMenu(menuName = MENU_NAME)]
public class MortarWeaponBalanceData : StaticDataReferenceBalanceData<
    SpecificationDataSource, 
    Specification,
    Mortar,
    MortarWeaponBalanceObject>
{
    private const string MENU_NAME =  BalanceDataUtilites.BALANCE_MENU_FOLDER + "MortarWeaponBalanceData";

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
