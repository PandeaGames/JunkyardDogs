using System;
using System.Collections.Generic;
using JunkyardDogs.Data.Balance;
using UnityEngine;

[Serializable]
public class CurrencyDataBalanceObject : IStaticDataBalanceObject
{
    public string name;

    public string tag_01;
    public string tag_02;
    public string tag_03;
    public string tag_04;
    public string tag_05;
    
    public string GetDataUID()
    {
        return name;
    }
}

[CreateAssetMenu]
public class CurrencyData : AbstractStaticData, ILoot, IStaticDataBalance<CurrencyDataBalanceObject>
{
    [SerializeField]
    private List<string> _tags;
    public List<string> Tags
    {
        get => _tags;
    }
    
    public void ApplyBalance(CurrencyDataBalanceObject balance)
    {
        _tags = new List<string>();
        name = balance.name;

        ImportTag(_tags, balance.tag_01);
        ImportTag(_tags, balance.tag_02);
        ImportTag(_tags, balance.tag_03);
        ImportTag(_tags, balance.tag_04);
        ImportTag(_tags, balance.tag_05);
    }

    public CurrencyDataBalanceObject GetBalance()
    {
        CurrencyDataBalanceObject balance = new CurrencyDataBalanceObject();

        balance.name = name;

        return balance;
    }
    
    private void ImportTag(List<string> list, string tag)
    {
        if (!string.IsNullOrEmpty(tag))
        {
            list.Add(tag);
        }
    }
}
