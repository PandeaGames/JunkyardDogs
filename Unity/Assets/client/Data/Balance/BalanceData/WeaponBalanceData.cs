
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
    public float stun;
}

[CreateAssetMenu(menuName = MENU_NAME)]
public class WeaponBalanceData : StaticDataReferenceBalanceData<
    SpecificationDataSource, 
    Specification,
    Weapon,
    WeaponBalanceObject>
{
    private const string MENU_NAME =  BalanceDataUtilites.BALANCE_MENU_FOLDER + "WeaponBalanceData";

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
