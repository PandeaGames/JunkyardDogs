using PandeaGames.Data.Static;
using UnityEngine;

[CreateAssetMenu(menuName = MENU_NAME)]
public class JunkyardDataSource : AbstractScriptableObjectStaticData<JunkyardData>
{
    private const string MENU_NAME =  BalanceDataUtilites.BALANCE_SOURCE_MENU_FOLDER + "Junkyard Data";
}