
using System;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using JunkyardDogs.Specifications;
using UnityEngine;

[Serializable]
public class PulseWeaponBalanceObject:PhysicalSpecificationBalanceObject
{
    public double cooldown;
    public double chargeTime;
    public float speed;
    public float radius;
    public int damage;
}

[CreateAssetMenu(menuName = MENU_NAME)]
public class PulseWeaponBalanceData : StaticDataReferenceBalanceData<
    SpecificationDataSource, 
    Specification,
    PulseEmitter,
    PulseWeaponBalanceObject>
{
    private const string MENU_NAME =  BalanceDataUtilites.BALANCE_MENU_FOLDER + "PulseWeaponBalanceData";

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
