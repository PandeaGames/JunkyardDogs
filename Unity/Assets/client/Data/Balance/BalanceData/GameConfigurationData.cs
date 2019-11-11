using UnityEngine;

namespace JunkyardDogs.Data.Balance
{
    [CreateAssetMenu(menuName = MENU_NAME)]
    public class GameConfigurationData : StaticDataReferenceBalanceData<GameConfigStaticDataList, GameStaticData, GameStaticData, GameConfigurationDataBalanceObject>
    {
        private const string MENU_NAME =  BalanceDataUtilites.BALANCE_MENU_FOLDER + "GameConfigurationData";
        public override string GetUIDFieldName()
        {
            return "name";
        }

        protected override string GetNewDataFolder()
        {
            return "";
        }
    }
}