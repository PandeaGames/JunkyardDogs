using PandeaGames.Data.Static;
using UnityEditor;
using UnityEngine;
using Material = JunkyardDogs.Specifications.Material;

namespace JunkyardDogs.Data
{
    [CreateAssetMenu(menuName = MENU_NAME)]
    public class MaterialDataSource : AbstractScriptableObjectStaticData<Material>
    {
        private const string MENU_NAME =  BalanceDataUtilites.BALANCE_SOURCE_MENU_FOLDER + "Materials";
    }
}