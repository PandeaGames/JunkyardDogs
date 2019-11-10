using System;
using UnityEngine;

public interface IExperienceModel
{
    int GetExp(Nationality nationality);
    void AddExp(Nationality nationality, int amount);
    int GetTotalExp();
}

[Serializable]
public class Experience : IExperienceModel
{
    [SerializeField]
    private NationDictionary _nationDictionary;
    public NationDictionary NationDictionary
    {
        get { return _nationDictionary; }
        set{_nationDictionary = value;}
    }
    
    public Experience()
    {
        _nationDictionary = new NationDictionary();
    }
    
    public int GetExp(Nationality nationality)
    {
        if (_nationDictionary.Contains(nationality))
        {
            return _nationDictionary[nationality].Exp;
        }

        return 0;
    }

    public void AddExp(Nationality nationality, int amount)
    {
        if (_nationDictionary.Contains(nationality))
        {
            amount += _nationDictionary[nationality].Exp;
        }
        
        _nationDictionary[nationality].Exp = amount;
    }
    
    public int GetTotalExp()
    {
        int total = 0;

        foreach (NationDictionaryKvP kvp in _nationDictionary.KeyValuePairs)
        {
            total += kvp.Value.Exp;
        }

        return total;
    }
}
