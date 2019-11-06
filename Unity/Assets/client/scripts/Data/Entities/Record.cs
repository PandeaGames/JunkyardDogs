using UnityEngine;
using System.Collections;

public class Record
{
    [SerializeField]
    private int _wins;
    
    [SerializeField]
    private int _losses;

    public int Wins
    {
        get { return _wins;}
        set { _wins = value; }
    }

    public int Losses
    {
        get { return _losses;}
        set { _losses = value; }
    }
}
