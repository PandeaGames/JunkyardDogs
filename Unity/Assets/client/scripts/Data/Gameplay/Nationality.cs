using UnityEngine;
using System.Collections;
using JunkyardDogs.Data.Balance;

[CreateAssetMenu(fileName = "Nationality", menuName = "GamePlay/Nationality", order = 3)]
public class Nationality : ScriptableObject, IStaticDataBalance<NationBalanceObject>
{
    public void ApplyBalance(NationBalanceObject balance)
    {
        this.name = balance.name;
    }

    public NationBalanceObject GetBalance()
    {
        NationBalanceObject balance = new NationBalanceObject();
        balance.name = this.name;
        return balance;
    }
}