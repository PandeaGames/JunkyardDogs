using System;
using System.Collections.Generic;
using JunkyardDogs.Data;
using UnityEngine;

[Serializable]
public class Wallet
{
    [SerializeField] 
    private List<Currency> _currency;
    
    public List<Currency> Currency { get { return _currency; } set { _currency = value; } }

    public Wallet()
    {
        Currency = new List<Currency>();
    }

    public void Add(Currency currencyIn)
    {
        Currency currency = GetCurrency(currencyIn.CurrencyType);
        currency.Quantity += currencyIn.Quantity;
    }
    
    public void Remove(Currency currencyOut)
    {
        Currency currency = GetCurrency(currencyOut.CurrencyType);
        currency.Quantity -= currencyOut.Quantity;
    }
    
    public Currency GetCurrency(CurrencyStaticDataReference currencyType)
    {
        foreach (Currency currency in Currency)
        {
            if (currency.CurrencyType.Equals(currencyType))
            {
                return currency;
            }
        }

        Currency emptyCurrency = new Currency();
        emptyCurrency.CurrencyType = currencyType;
        
        Currency.Add(emptyCurrency);

        return emptyCurrency;
    }
}