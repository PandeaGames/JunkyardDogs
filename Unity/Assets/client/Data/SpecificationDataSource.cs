using JunkyardDogs.Specifications;
using PandeaGames.Data.Static;
using UnityEngine;

namespace JunkyardDogs.Data
{
    [CreateAssetMenu(menuName = MENU_NAME)]
    public class SpecificationDataSource : AbstractScriptableObjectStaticData<Specification>
    {
        private const string MENU_NAME =  BalanceDataUtilites.BALANCE_SOURCE_MENU_FOLDER + "Specifications";
    }
}