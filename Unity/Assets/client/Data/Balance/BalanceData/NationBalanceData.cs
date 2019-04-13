
using System;
using UnityEngine;

namespace JunkyardDogs.Data.Balance
{
    [Serializable]
    public struct NationBalanceObject:IStaticDataBalanceObject
    {
        public string name;
        
        public string distinctionId_01;
        public int distinctionValue_01;
        public string distinctionId_02;
        public int distinctionValue_02;
        public string distinctionId_03;
        public int distinctionValue_03;
        public string distinctionId_04;
        public int distinctionValue_04;
        public string distinctionId_05;
        public int distinctionValue_05;
        
        public string GetDataUID()
        {
            return name;
        }
    }
    
    [CreateAssetMenu(menuName = MENU_NAME)]
    public class NationBalanceData : StaticDataReferenceBalanceData<NationalityDataSource, Nationality, Nationality, NationBalanceObject>
    {
        private const string MENU_NAME =  BalanceDataUtilites.BALANCE_MENU_FOLDER + "NationBalanceData";

        public const string NATION_DATA_PATH = "Assets/AssetBundles/Data/Nations/";
        
        public override string GetUIDFieldName()
        {
            return "name";
        }

        protected override string GetNewDataFolder()
        {
            return NATION_DATA_PATH;
        }
    }
}