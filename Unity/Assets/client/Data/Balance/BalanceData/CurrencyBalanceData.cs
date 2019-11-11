using JunkyardDogs.Data.Balance;
using UnityEngine;

[CreateAssetMenu(menuName = MENU_NAME)]
public class CurrencyBalanceData : StaticDataReferenceBalanceData<CurrencyDataSource, CurrencyData, CurrencyData, CurrencyDataBalanceObject>
{
    private const string MENU_NAME =  BalanceDataUtilites.BALANCE_MENU_FOLDER + "Currency";
    private const string DATA_PATH = "Assets/AssetBundles/Data/Currency/";
    
    public override string GetUIDFieldName()
    {
        return "name";
    }

    protected override string GetNewDataFolder()
    {
        return DATA_PATH;
    }
}
