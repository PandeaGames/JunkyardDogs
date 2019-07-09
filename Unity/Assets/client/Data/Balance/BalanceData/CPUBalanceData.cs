
using System;
using JunkyardDogs.Specifications;
using UnityEngine;

namespace JunkyardDogs.Data.Balance
{
    [Serializable]
    public struct CPUBalanceObject:IStaticDataBalanceObject
    {
        public string name;
        
        public string distinctionId_01;
        public int distinctionValue_01;
        public string distinctionId_02;
        public int distinctionValue_02;
        public string distinctionId_03;
        public int distinctionValue_03;

        public int aggressiveness;
        public int evasiveness;
        public int cautiousness;
        public int directiveSlotCount;
        
        public string GetDataUID()
        {
            return name;
        }
    }
    
    [CreateAssetMenu(menuName = MENU_NAME)]
    public class CPUBalanceData : StaticDataReferenceBalanceData<SpecificationDataSource, Specification, CPU, CPUBalanceObject>
    {
        private const string MENU_NAME =  BalanceDataUtilites.BALANCE_MENU_FOLDER + "CPU";
        private const string CPU_DATA_PATH = "Assets/AssetBundles/Data/CPUs/";
        
        public override string GetUIDFieldName()
        {
            return "name";
        }

        protected override string GetNewDataFolder()
        {
            return CPU_DATA_PATH;
        }
    }
}