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
    
    public override ILoot[] GetLoot(ILootDataModel dataModel)
    {
        ILoot[] lootData = new ILoot[_lootData.Length];

        for (int i = 0; i < _lootData.Length; i++)
        {
            lootData[i] = _lootData[i].Data.GetLoot(dataModel);
            if (lootData[i] == null)
            {
                Debug.LogError($"{nameof(LootCrateData)} produced a NULL loot item from {_lootData[i].Data.name} at index {i}");
            }
        }

        return lootData;
    }

    public void ApplyBalance(LootCrateBalanceObject balance)
    {
        name = balance.name;
        List<LootStaticDataReference> list = new List<LootStaticDataReference>();

        ImportLoot(list, balance.Loot01);
        ImportLoot(list, balance.Loot02);
        ImportLoot(list, balance.Loot03);
        ImportLoot(list, balance.Loot04);
        ImportLoot(list, balance.Loot05);
        ImportLoot(list, balance.Loot06);
        ImportLoot(list, balance.Loot07);
        ImportLoot(list, balance.Loot08);
        ImportLoot(list, balance.Loot09);
        ImportLoot(list, balance.Loot10);
        ImportLoot(list, balance.Loot11);
        ImportLoot(list, balance.Loot12);
        ImportLoot(list, balance.Loot13);
        ImportLoot(list, balance.Loot14);
        ImportLoot(list, balance.Loot15);
        ImportLoot(list, balance.Loot16);
        ImportLoot(list, balance.Loot17);
        ImportLoot(list, balance.Loot18);
        ImportLoot(list, balance.Loot19);
        ImportLoot(list, balance.Loot20);
        
        _lootData = list.ToArray();
    }

    private void ImportLoot(List<LootStaticDataReference> list, string lootId)
    {
        if (!string.IsNullOrEmpty(lootId))
        {
            LootStaticDataReference reference = new LootStaticDataReference();
            reference.ID = lootId;
            list.Add(reference);
        }
    }

    public LootCrateBalanceObject GetBalance()
    {
        LootCrateBalanceObject balance = new LootCrateBalanceObject();
        balance.name = name;
        //balance.loot = string.Join(BalanceData.ListDelimiter, new List<LootStaticDataReference>(_lootData));

        return balance;
    }
}
