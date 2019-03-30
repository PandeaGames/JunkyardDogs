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
        string[] weightedLootCrateItemBalances = balance.loot.Split(BalanceData.ListDelimiterChar);
        _loot = new WeightedLootCrateItem[weightedLootCrateItemBalances.Length];

        for (int i = 0; i < _loot.Length; i++)
        {
            string[] data = weightedLootCrateItemBalances[i].Split(BalanceData.DataDelimiterChar);
            WeightedLootCrateItem item = new WeightedLootCrateItem();
            LootStaticDataReference lootItemReference = new LootStaticDataReference();
            lootItemReference.ID = data[0];
            item.weight = int.Parse(data[1]);
            _loot[i] = item;
        }

        _lootQuantity = balance.lootQuantity;
        _pickWithRepetition = balance.pickWithRepetition;
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

        balance.loot = string.Join(BalanceData.ListDelimiter, lootItems);
        balance.lootQuantity = _lootQuantity;
        balance.pickWithRepetition = _pickWithRepetition;
        
        return balance;
    }
}
