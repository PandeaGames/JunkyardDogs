using System;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using UnityEngine;

[Serializable]
public class CurrencyLootBalanceObject : IStaticDataBalanceObject
{
    public string name;
    public string currency;
    public int min;
    public int max;
    public string GetDataUID()
    {
        return name;
    }
}

public class CurrencyLoot : LootData<CurrencyData>, IStaticDataBalance<CurrencyLootBalanceObject>
{
    [SerializeField, CurrencyStaticDataReference]
    private CurrencyStaticDataReference _currencyType;
    
    public int min;
    public int max;
    
    public override ILoot GetLoot(ILootDataModel dataModel)
    {
        Currency currencyLoot = new Currency();
        currencyLoot.Quantity = UnityEngine.Random.Range(min, max);
        currencyLoot.CurrencyType = _currencyType;
        return currencyLoot;
    }

    public void ApplyBalance(CurrencyLootBalanceObject balance)
    {
        this.name = balance.name;
        this.min = balance.min;
        this.max = balance.max;
        this._currencyType = new CurrencyStaticDataReference();
        this._currencyType.ID = balance.currency;
    }

    public CurrencyLootBalanceObject GetBalance()
    {
        throw new NotImplementedException();
    }
}
