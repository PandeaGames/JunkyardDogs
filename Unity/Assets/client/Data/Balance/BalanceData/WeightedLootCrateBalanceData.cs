using System;
using JunkyardDogs.Data.Balance;
using UnityEngine;

[Serializable]
public struct WeightedLootCrateBalanceObject:IStaticDataBalanceObject
{
    public string name;
    public string loot;
    public int lootQuantity;
    public bool pickWithRepetition;
    
    public string GetDataUID()
    {
        return name;
    }
}

[CreateAssetMenu]
public class WeightedLootCrateBalanceData : StaticDataReferenceBalanceData<
    LootCrateDataSource, 
    AbstractLootCrateData, 
    WeightedLootCrateBalanceObject,
    WeightedLootCrateData,
    WeightedLootCrateBalanceObject>
{
    public const string DATA_PATH = "Assets/AssetBundles/Data/Loot/LootCrates/";
    
    public override string GetUIDFieldName()
    {
        return "name";
    }

    protected override string GetNewDataFolder()
    {
        return DATA_PATH;
    }
}
