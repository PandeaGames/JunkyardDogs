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
    public string material;
    
    public string GetDataUID()
    {
        return name;
    }
}

[CreateAssetMenu(menuName = MENU_NAME)]
public class WeaponBlueprintBalanceData : StaticDataReferenceBalanceData<
    BlueprintDataSource, 
    BlueprintDataBase,
    WeaponBlueprintData, 
    WeaponBlueprintBalanceObject>
{
    private const string MENU_NAME =  BalanceDataUtilites.BALANCE_MENU_FOLDER + "WeaponBlueprintBalanceData";

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
