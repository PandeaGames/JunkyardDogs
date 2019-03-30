using JunkyardDogs.Data.Balance;
using UnityEngine;

public class BlueprintDataBase : ScriptableObject, IStaticDataBalance<BlueprintBalanceObject>, ILoot
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