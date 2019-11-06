using System;
using JunkyardDogs.Data;
using UnityEngine;

[Serializable]
public class Currency:ILoot
{
    [SerializeField, CurrencyStaticDataReference]
    private CurrencyStaticDataReference _currencyType;
    
    [SerializeField]
    private int _quantity;
    
    public CurrencyStaticDataReference CurrencyType { get { return _currencyType; } set { _currencyType = value; } }
    public int Quantity { get { return _quantity; } set { _quantity = value; } }
}
