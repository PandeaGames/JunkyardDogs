using System;
using JunkyardDogs.Data.Balance;
using UnityEngine;

[Serializable]
public struct WeightedLootCrateBalanceObject:IStaticDataBalanceObject
{
    public string name;
    public int lootQuantity;
    public bool pickWithRepetition;

    public string Loot01;
    public string Loot02;
    public string Loot03;
    public string Loot04;
    public string Loot05;
    public string Loot06;
    public string Loot07;
    public string Loot08;
    public string Loot09;
    public string Loot10;
    public string Loot11;
    public string Loot12;
    public string Loot13;
    public string Loot14;
    public string Loot15;
    public string Loot16;
    public string Loot17;
    public string Loot18;
    public string Loot19;
    public string Loot20;
    public string Loot21;
    public string Loot22;
    public string Loot23;
    public string Loot24;
    public string Loot25;
    public string Loot26;
    public string Loot27;
    public string Loot28;
    public string Loot29;
    public string Loot30;
    
    public int LootWeight01;
    public int LootWeight02;
    public int LootWeight03;
    public int LootWeight04;
    public int LootWeight05;
    public int LootWeight06;
    public int LootWeight07;
    public int LootWeight08;
    public int LootWeight09;
    public int LootWeight10;
    public int LootWeight11;
    public int LootWeight12;
    public int LootWeight13;
    public int LootWeight14;
    public int LootWeight15;
    public int LootWeight16;
    public int LootWeight17;
    public int LootWeight18;
    public int LootWeight19;
    public int LootWeight20;
    public int LootWeight21;
    public int LootWeight22;
    public int LootWeight23;
    public int LootWeight24;
    public int LootWeight25;
    public int LootWeight26;
    public int LootWeight27;
    public int LootWeight28;
    public int LootWeight29;
    public int LootWeight30;
  
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
