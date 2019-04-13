using System;
using UnityEngine;

namespace JunkyardDogs.Data.Balance
{
    [Serializable]
    public struct TournamentFormatBalanceObject:IStaticDataBalanceObject
    {
        public string name;
        public int participants;
        public string stage_01;
        public string stage_02;
        public string stage_03;
        public string stage_04;
        public string stage_05;
    
        public string GetDataUID()
        {
            return name;
        }
    }
    
    [CreateAssetMenu(menuName = MENU_NAME)]
    public class TournamentFormatBalanceData : StaticDataReferenceBalanceData<
        TournamentFormatDataSource, 
        TournamentFormat,
        TournamentFormat, 
        TournamentFormatBalanceObject>
    {
        private const string MENU_NAME =  BalanceDataUtilites.BALANCE_MENU_FOLDER + "TournamentFormatBalanceData";

        public const string DATA_PATH = "Assets/AssetBundles/Data/Tournaments/Formats/";

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