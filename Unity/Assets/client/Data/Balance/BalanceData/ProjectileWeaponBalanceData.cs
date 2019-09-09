
using System;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using JunkyardDogs.Specifications;
using UnityEngine;

[Serializable]
public class ProjectileWeaponBalanceObject:PhysicalSpecificationBalanceObject
{
    public double cooldown;
    public double chargeTime;
    public float speed;
    public float radius;
    public int damage;
    public int armourPiercing;
    public float stun;
}

[CreateAssetMenu(menuName = MENU_NAME)]
public class ProjectileWeaponBalanceData : StaticDataReferenceBalanceData<
    SpecificationDataSource, 
    Specification,
    ProjectileWeapon,
    ProjectileWeaponBalanceObject>
{
    private const string MENU_NAME =  BalanceDataUtilites.BALANCE_MENU_FOLDER + "ProjectileWeaponBalanceData";

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
