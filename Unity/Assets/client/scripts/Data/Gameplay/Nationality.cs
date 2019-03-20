using UnityEngine;
using System.Collections;
using JunkyardDogs.Data.Balance;

[CreateAssetMenu(fileName = "Nationality", menuName = "GamePlay/Nationality", order = 3)]
public class Nationality : ScriptableObject
{
    #if UNITY_EDITOR
    public void ApplyBalance(NationBalanceObject balance)
    {
        this.name = balance.name;
    }
    #endif
}