using System;
using JunkyardDogs.Data.Balance;
using UnityEngine;

[Serializable]
public struct LootCrateBalanceObject:IStaticDataBalanceObject
{
    public string name;

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
    
    public string GetDataUID()
    {
        return name;
    }
}

[CreateAssetMenu]
public class LootCrateBalanceData : StaticDataReferenceBalanceData<
    LootCrateDataSource, 
    AbstractLootCrateData, 
    LootCrateBalanceObject,
    LootCrateData,
    LootCrateBalanceObject>
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
