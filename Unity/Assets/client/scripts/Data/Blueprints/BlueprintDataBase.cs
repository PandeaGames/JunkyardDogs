using JunkyardDogs.Data.Balance;
using UnityEngine;

public class BlueprintDataBase : ScriptableObject, IStaticDataBalance<BlueprintBalanceObject>
{
    public void ApplyBalance(BlueprintBalanceObject balance)
    {
        name = balance.name;
    }

    public BlueprintBalanceObject GetBalance()
    {
        BlueprintBalanceObject balance = new BlueprintBalanceObject();
        balance.name = name;
        return balance;
    }
}