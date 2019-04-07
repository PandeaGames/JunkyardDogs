using PandeaGames.Data.Static;
using UnityEngine;

[CreateAssetMenu(menuName = MENU_NAME)]
public class CurrencyDataSource : AbstractScriptableObjectStaticData<CurrencyData>
{
    private const string MENU_NAME =  BalanceDataUtilites.BALANCE_SOURCE_MENU_FOLDER + "Currency";
}
