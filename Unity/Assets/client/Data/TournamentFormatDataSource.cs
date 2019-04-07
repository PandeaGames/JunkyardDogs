using PandeaGames.Data.Static;
using UnityEngine;

namespace JunkyardDogs.Data
{
    [CreateAssetMenu(menuName = MENU_NAME)]
    public class TournamentFormatDataSource : AbstractScriptableObjectStaticData<TournamentFormat>
    {
        private const string MENU_NAME =  BalanceDataUtilites.BALANCE_SOURCE_MENU_FOLDER + "Tournament Formats";
    }
}