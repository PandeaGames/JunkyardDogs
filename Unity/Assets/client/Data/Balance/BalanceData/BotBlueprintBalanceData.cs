using System;
using UnityEngine;

namespace JunkyardDogs.Data.Balance
{
    [Serializable]
    public struct BotBlueprintBalanceObject:IStaticDataBalanceObject
    {
        public string name;
        public string motherboard;
        public string chassis;
        public string agent;
        public string manufacturer;
        
        public string GetDataUID()
        {
            return name;
        }
    }
    
    [CreateAssetMenu(menuName = MENU_NAME)]
    public class BotBlueprintBalanceData : StaticDataReferenceBalanceData<
        BlueprintDataSource, 
        BlueprintDataBase, 
        BotBlueprintData, 
        BotBlueprintBalanceObject>
    {
        private const string MENU_NAME =  BalanceDataUtilites.BALANCE_MENU_FOLDER + "BotBlueprintBalanceData";
        public const string DATA_PATH = "Assets/AssetBundles/Data/Blueprints/Bots/";
    
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