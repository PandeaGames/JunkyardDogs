using System;
using JunkyardDogs.Specifications;
using UnityEngine;

namespace JunkyardDogs.Data.Balance
{
    [Serializable]
    public struct DirectiveBalanceObject:IStaticDataBalanceObject
    {
        public string name;
        public string directive;
        
        public string GetDataUID()
        {
            return name;
        }
    }
    
    [CreateAssetMenu(menuName = MENU_NAME)]
    public class DirectiveBalanceData : StaticDataReferenceBalanceData<SpecificationDataSource, Specification, Directive, DirectiveBalanceObject>
    {
        private const string MENU_NAME =  BalanceDataUtilites.BALANCE_MENU_FOLDER + "Directive";
        private const string DATA_PATH = "Assets/AssetBundles/Data/Directives/";
        
        public override string GetUIDFieldName()
        {
            return "name";
        }

        protected override string GetNewDataFolder()
        {
            return DATA_PATH;
        }
    }
}