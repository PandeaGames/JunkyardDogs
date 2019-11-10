using System;
using UnityEngine;

[Serializable]
public class NationalExperience
{
    [SerializeField]
    private int _exp;
    
    public int Exp
    {
        get { return _exp;}
        set { _exp = value; }
    }

    public void AddExp(int amount)
    {
        Exp += amount;
    }
}
