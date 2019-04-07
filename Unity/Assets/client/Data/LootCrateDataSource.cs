using PandeaGames.Data.Static;
using UnityEngine;

[CreateAssetMenu(menuName = MENU_NAME)]
public class LootCrateDataSource : AbstractScriptableObjectStaticData<AbstractLootCrateData>
{
    private const string MENU_NAME =  BalanceDataUtilites.BALANCE_SOURCE_MENU_FOLDER + "Loot Crates";
}