using System;
using System.Collections.Generic;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using UnityEngine;

[Serializable]
public struct WeightedLootCrateItem
{
    [LootStaticDataReference]
    public LootStaticDataReference loot;
    public int weight;
}

[Serializable, CreateAssetMenu]
public class WeightedLootCrateData : AbstractLootCrateData, IStaticDataBalance<WeightedLootCrateBalanceObject>
{
    [SerializeField] 
    private WeightedLootCrateItem[] _loot;

    [SerializeField] 
    private int _lootQuantity;

    [SerializeField]
    private bool _pickWithRepetition;
    
    public override ILoot[] GetLoot()
    {
        bool isLootCrateValid = !(_loot.Length < _lootQuantity && !_pickWithRepetition);
        
        if (isLootCrateValid)
        {
            ILoot[] loot = new ILoot[_lootQuantity];
            
            List<WeightedLootCrateItem> lootChoices = new List<WeightedLootCrateItem>(_loot);

            int totalWeight = 0;

            foreach (WeightedLootCrateItem lootChoice in lootChoices)
            {
                totalWeight += lootChoice.weight;
            }
            
            int lootChosen = 0;
            do
            {
                int weightedChoice = UnityEngine.Random.Range(0, totalWeight);
                int searchTotal = 0;
                int indexFound = 0;
                WeightedLootCrateItem item = default(WeightedLootCrateItem);
                
                for (int i = 0; i < lootChoices.Count; i++)
                {
                    item = lootChoices[i];
                    searchTotal += item.weight;

                    if (searchTotal > weightedChoice)
                    {
                        item = lootChoices[i];
                        indexFound = i;
                        break;
                    }
                }
                
                lootChoices.RemoveAt(indexFound);
                loot[lootChosen] = item.loot.Data.GetLoot();

            } while (++lootChosen < _lootQuantity);

            return loot;
        }
        else
        {
            throw new ArgumentOutOfRangeException("This crate '{0}' does not pick with repetition, and it does not have enough unique items to fill pick request.");
        }
    }

    public void ApplyBalance(WeightedLootCrateBalanceObject balance)
    {

        List<WeightedLootCrateItem> list = new List<WeightedLootCrateItem>();

        ImportLootBalance(list, balance.Loot01, balance.LootWeight01);
        ImportLootBalance(list, balance.Loot02, balance.LootWeight02);
        ImportLootBalance(list, balance.Loot03, balance.LootWeight03);
        ImportLootBalance(list, balance.Loot04, balance.LootWeight04);
        ImportLootBalance(list, balance.Loot05, balance.LootWeight05);
        ImportLootBalance(list, balance.Loot06, balance.LootWeight06);
        ImportLootBalance(list, balance.Loot07, balance.LootWeight07);
        ImportLootBalance(list, balance.Loot08, balance.LootWeight08);
        ImportLootBalance(list, balance.Loot09, balance.LootWeight09);
        ImportLootBalance(list, balance.Loot10, balance.LootWeight10);
        ImportLootBalance(list, balance.Loot11, balance.LootWeight11);
        ImportLootBalance(list, balance.Loot12, balance.LootWeight12);
        ImportLootBalance(list, balance.Loot13, balance.LootWeight13);
        ImportLootBalance(list, balance.Loot14, balance.LootWeight14);
        ImportLootBalance(list, balance.Loot15, balance.LootWeight15);
        ImportLootBalance(list, balance.Loot16, balance.LootWeight16);
        ImportLootBalance(list, balance.Loot17, balance.LootWeight17);
        ImportLootBalance(list, balance.Loot18, balance.LootWeight18);
        ImportLootBalance(list, balance.Loot19, balance.LootWeight19);
        ImportLootBalance(list, balance.Loot20, balance.LootWeight20);
        ImportLootBalance(list, balance.Loot21, balance.LootWeight21);
        ImportLootBalance(list, balance.Loot22, balance.LootWeight22);
        ImportLootBalance(list, balance.Loot23, balance.LootWeight23);
        ImportLootBalance(list, balance.Loot24, balance.LootWeight24);
        ImportLootBalance(list, balance.Loot25, balance.LootWeight25);
        ImportLootBalance(list, balance.Loot26, balance.LootWeight26);
        ImportLootBalance(list, balance.Loot27, balance.LootWeight27);
        ImportLootBalance(list, balance.Loot28, balance.LootWeight28);
        ImportLootBalance(list, balance.Loot29, balance.LootWeight29);
        ImportLootBalance(list, balance.Loot30, balance.LootWeight30);

        _lootQuantity = balance.lootQuantity;
        _pickWithRepetition = balance.pickWithRepetition;
        _loot = list.ToArray();
    }

    private void ImportLootBalance(List<WeightedLootCrateItem> list, string lootId, int weight)
    {
        if (!string.IsNullOrEmpty(lootId))
        {
            WeightedLootCrateItem item = new WeightedLootCrateItem();
            LootStaticDataReference lootItemReference = new LootStaticDataReference();
            lootItemReference.ID = lootId;
            item.weight = weight;
            list.Add(item);
        }
    }

    public WeightedLootCrateBalanceObject GetBalance()
    {
        WeightedLootCrateBalanceObject balance = new WeightedLootCrateBalanceObject();
        balance.name = name;

        string[] lootItems = new string[_loot.Length];

        for (int i = 0; i < _loot.Length; i++)
        {
            lootItems[i] = string.Join(BalanceData.DataDelimiter, new string[] {_loot[i].loot.ID, _loot[i].weight.ToString()});
        }

        balance.lootQuantity = _lootQuantity;
        balance.pickWithRepetition = _pickWithRepetition;
        
        return balance;
    }
}
