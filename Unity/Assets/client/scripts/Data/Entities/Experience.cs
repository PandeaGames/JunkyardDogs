using System;
using JunkyardDogs.Components.Gameplay;
using UnityEngine;

public interface IExperienceModel
{
    int GetExp(Nationality nationality);
    uint GetLevel(Nationality nationality);
    void AddExp(Nationality nationality, int amount);
    void Ascend(Nationality nationality);
    uint Ascend();
    int GetTotalExp();
}

[Serializable]
public class Experience : ExpLevel, IExperienceModel
{
    [SerializeField]
    private NationDictionary _nationDictionary = new NationDictionary();
    public NationDictionary NationDictionary
    {
        get { return _nationDictionary; }
        set{_nationDictionary = value;}
    }

    public void Ascend(Nationality nationality)
    {
        if (!_nationDictionary.Contains(nationality))
        {
            _nationDictionary.AddValue(nationality, new NationalExperience(0));
        }

        _nationDictionary.GetValue(nationality).Ascend();
    }

    public int GetExp(Nationality nationality)
    {
        if (_nationDictionary.Contains(nationality))
        {
            return _nationDictionary.GetValue(nationality);
        }

        return 0;
    }

    public uint GetLevel(Nationality nationality)
    {
        if (_nationDictionary.Contains(nationality))
        {
            return _nationDictionary.GetValue(nationality).Level;
        }

        return 0;
    }
    
    public void AddExp(int amount)
    {
        Value += amount;
    }

    public void AddExp(Nationality nationality, int amount)
    {
        if (_nationDictionary.Contains(nationality))
        {
            amount += _nationDictionary.GetValue(nationality);
        }
        
        _nationDictionary.SetValue(nationality, amount);
        AddExp(amount);
    }
    
    public int GetTotalExp()
    {
        int total = 0;

        foreach (NationDictionaryKvP kvp in _nationDictionary.KeyValuePairs)
        {
            total += kvp.Value;
        }

        return total;
    }
}
