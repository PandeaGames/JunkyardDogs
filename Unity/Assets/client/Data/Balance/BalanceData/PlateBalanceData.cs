using System;
using JunkyardDogs.Specifications;
using UnityEngine;

namespace JunkyardDogs.Data.Balance
{
    [Serializable]
    public class PlateBalanceObject : PhysicalSpecificationBalanceObject
    {
        public float thickness;
        public float armour;
    }
    
    [CreateAssetMenu(menuName = MENU_NAME)]
    public class PlateBalanceData : StaticDataReferenceBalanceData<
        SpecificationDataSource, 
        Specification,
        Plate, 
        PlateBalanceObject>
    {

        private const string MENU_NAME =  BalanceDataUtilites.BALANCE_MENU_FOLDER + "PlateBalanceData";
        public const string DATA_PATH = "Assets/AssetBundles/Data/Products/Plating/";

        protected override string GetNewDataFolder()
        {
            return DATA_PATH;
        }

        public override string GetUIDFieldName()
        {
            return "name";
        }
    }
}