
using JunkyardDogs.Data.Balance;
using UnityEngine;

[CreateAssetMenu(menuName = MENU_NAME)]
public class CurrencyLootBalanceData : StaticDataReferenceBalanceData<
    LootDataSource, 
    AbstractLootData,
    CurrencyLoot,
    CurrencyLootBalanceObject>
{
    private const string MENU_NAME =  BalanceDataUtilites.BALANCE_MENU_FOLDER + "Currency Loot";
    private const string DATA_PATH = BlueprintLootBalanceData.DATA_PATH;
        
    public override string GetUIDFieldName()
    {
        return "name";
    }

    protected override string GetNewDataFolder()
    {
        return DATA_PATH;
    }
}
