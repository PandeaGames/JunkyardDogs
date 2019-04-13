
using System;
using JunkyardDogs.Data.Balance;
using UnityEngine;


[Serializable]
public struct LootBalanceObject:IStaticDataBalanceObject
{
    public string name;
    public string loot;
    
    public string GetDataUID()
    {
        return name;
    }
}

[CreateAssetMenu(menuName = MENU_NAME)]
public class LootBalanceData : StaticDataReferenceBalanceData<
    LootCrateDataSource, 
    AbstractLootCrateData,
    LootCrateData,
    LootCrateBalanceObject>
{
    private const string MENU_NAME =  BalanceDataUtilites.BALANCE_MENU_FOLDER + "LootBalanceData";

    public const string DATA_PATH = "Assets/AssetBundles/Data/Loot/";
    
    public override string GetUIDFieldName()
    {
        return "name";
    }

    protected override string GetNewDataFolder()
    {
        return DATA_PATH;
    }
}