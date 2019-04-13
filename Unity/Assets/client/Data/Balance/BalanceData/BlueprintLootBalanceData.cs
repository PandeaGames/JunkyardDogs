using System;
using JunkyardDogs.Data.Balance;
using UnityEngine;

[Serializable]
public struct BlueprintLootBalanceObject:IStaticDataBalanceObject
{
    public string name;
    public string blueprintId;
    
    public string GetDataUID()
    {
        return name;
    }
}

[CreateAssetMenu(menuName = MENU_NAME)]
public class BlueprintLootBalanceData : StaticDataReferenceBalanceData<
    LootDataSource, 
    AbstractLootData,
    BlueprintLootData,
    BlueprintLootBalanceObject>
{
    private const string MENU_NAME =  BalanceDataUtilites.BALANCE_MENU_FOLDER + "BlueprintLootBalanceData";

    public const string DATA_PATH = "Assets/AssetBundles/Data/Loot/LootItems/";
    
    public override string GetUIDFieldName()
    {
        return "name";
    }

    protected override string GetNewDataFolder()
    {
        return DATA_PATH;
    }
}
