using System;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using UnityEngine;

[Serializable]
public struct ChassisBlueprintBalanceObject:IStaticDataBalanceObject
{
    public string name;
    public string specification;
    public string manufacturer;
    public string material;
    
    public string frontPlates;
    public string leftPlates;
    public string rightPlates;
    public string backPlates;
    public string topPlates;
    public string bottomPlates;

    public string topArmament;
    public string frontArmament;
    public string leftArmament;
    public string rightArmament;
    
    public string GetDataUID()
    {
        return name;
    }
}

[CreateAssetMenu(menuName = MENU_NAME)]
public class ChassisBlueprintBalanceData : StaticDataReferenceBalanceData<
    BlueprintDataSource, 
    BlueprintDataBase,
    ChassisBlueprintData, 
    ChassisBlueprintBalanceObject>
{
    private const string MENU_NAME =  BalanceDataUtilites.BALANCE_MENU_FOLDER + "ChassisBlueprintBalanceData";

    public const string DATA_PATH = "Assets/AssetBundles/Data/Blueprints/Components/Chassis/";
    
    public override string GetUIDFieldName()
    {
        return "name";
    }

    protected override string GetNewDataFolder()
    {
        return DATA_PATH;
    }
}
