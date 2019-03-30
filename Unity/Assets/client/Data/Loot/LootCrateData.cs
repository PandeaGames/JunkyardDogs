using System;
using System.Collections.Generic;
using UnityEngine;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;

[CreateAssetMenu, Serializable]
public class LootCrateData : AbstractLootCrateData, IStaticDataBalance<LootCrateBalanceObject>
{
    [SerializeField, LootStaticDataReference]
    private LootStaticDataReference[] _lootData;
    
    public override ILoot[] GetLoot()
    {
        ILoot[] lootData = new ILoot[_lootData.Length];

        for (int i = 0; i < _lootData.Length; i++)
        {
            lootData[i] = _lootData[i].Data.GetLoot();
        }

        return lootData;
    }

    public void ApplyBalance(LootCrateBalanceObject balance)
    {
        name = balance.name;

        string[] lootIds = balance.loot.Split(BalanceData.ListDelimiterChar);
        _lootData = new LootStaticDataReference[_lootData.Length];

        for (int i = 0;i<lootIds.Length; i++)
        {
            string lootId = lootIds[i];
            LootStaticDataReference reference = new LootStaticDataReference();
            reference.ID = lootId;
            _lootData[i] = reference;
        }
    }

    public LootCrateBalanceObject GetBalance()
    {
        LootCrateBalanceObject balance = new LootCrateBalanceObject();
        balance.name = name;
        balance.loot = string.Join(BalanceData.ListDelimiter, new List<LootStaticDataReference>(_lootData));

        return balance;
    }
}
