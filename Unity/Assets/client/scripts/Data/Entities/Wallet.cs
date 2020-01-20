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
        if (ContainsObj(key))
        {
            SetValue(key, GetValueByObj(key) + value);
        }
        else
        {
            SetValue(key, value);
        }
        
        return GetPair(key);
    }

    public Currency Add(Currency currency)
    {
        CurrencyStaticDataReference key = currency.CurrencyType;
        int value = currency.Quantity;
        if (ContainsObj(key))
        {
            SetValue(key, GetValueByObj(key) + value);
        }
        else
        {
            AddValue(key, value);
        }

        return currency;
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