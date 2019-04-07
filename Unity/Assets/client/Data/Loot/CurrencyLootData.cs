using JunkyardDogs.Data;
using UnityEngine;

public class CurrencyLootData : LootData<Currency>
{
    [SerializeField, CurrencyStaticDataReference]
    private CurrencyStaticDataReference _currencyType;

    [SerializeField]
    private int _quantity;
    
    public override ILoot GetLoot()
    {
        Currency currency = new Currency();
        currency.Quantity = _quantity;
        currency.CurrencyType = _currencyType;
        return currency;
    }
}
