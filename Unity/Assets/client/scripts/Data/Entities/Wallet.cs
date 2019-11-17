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
    public override CurrencyDictionaryKvP AddValue(CurrencyStaticDataReference key, int value)
    {
        return AddObj(key, value);
    }
    
    public override CurrencyDictionaryKvP AddValue(CurrencyData key, int value)
    {
        return AddObj(key, value);
    }
    
    public void Add(Currency currency)
    {
        AddObj(currency.CurrencyType, currency.Quantity);
    }
    
    private CurrencyDictionaryKvP AddObj(CurrencyData key, int value)
    {
        if (ContainsObj(key))
        {
            SetValueByObj(key, GetValueByObj(key) + value);
        }
        else
        {
            SetValueByObj(key, value);
        }

        return GetPair(key);
    }
}