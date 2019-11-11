using System;
using System.Collections.Generic;
using JunkyardDogs.Data;
using PandeaGames.Utils;
using UnityEngine;

[Serializable]
public class CurrencyDictionaryKvP : PandeaGames.Utils.KeyValuePair<CurrencyStaticDataReference, int>
{
    
}

[Serializable]
public class Wallet : DataReferenceDictionary<
    CurrencyStaticDataReference, 
    CurrencyData, 
    CurrencyData, 
    CurrencyDataProvider, 
    int, 
    CurrencyDictionaryKvP>
{
    public override void Add(CurrencyStaticDataReference key, int value)
    {
        Add(key, value);
    }
    
    public override void Add(CurrencyData key, int value)
    {
        Add(key, value);
    }
    
    public void Add(Currency currency)
    {
        Add(currency.CurrencyType, currency.Quantity);
    }
    
    private void Add(object key, int value)
    {
        if (Contains(key))
        {
            this[key] = this[key] + value;
        }
        else
        {
            this[key] = value;
        }
    }
}