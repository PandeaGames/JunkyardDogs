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
    
    public string frontPlates01;
    public string frontPlates02;
    public string frontPlates03;
    public string leftPlates01;
    public string leftPlates02;
    public string leftPlates03;
    public string rightPlates01;
    public string rightPlates02;
    public string rightPlates03;
    public string backPlates01;
    public string backPlates02;
    public string backPlates03;
    public string topPlates01;
    public string topPlates02;
    public string topPlates03;
    public string bottomPlates01;
    public string bottomPlates02;
    public string bottomPlates03;

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
