using System;
using JunkyardDogs.Data;

[Serializable]
public class Currency:ILoot
{
    public CurrencyStaticDataReference CurrencyType { get; set; }
    public int Quantity { get; set; }
}
