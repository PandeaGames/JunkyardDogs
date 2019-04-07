using JunkyardDogs.Behavior;
using PandeaGames.Data.Static;
using UnityEngine;
using JunkyardDogs.Simulation.Behavior;

namespace JunkyardDogs.Data
{
    [CreateAssetMenu(menuName = MENU_NAME)]
    public class ActionDataSource : AbstractScriptableObjectStaticData<BehaviorAction>
    {
        private const string MENU_NAME =  BalanceDataUtilites.BALANCE_SOURCE_MENU_FOLDER + "Actions";
    }
}