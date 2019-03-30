using System;
using UnityEngine;

namespace JunkyardDogs.Data.Balance
{
    [Serializable]
    public struct TournamentFormatBalanceObject:IStaticDataBalanceObject
    {
        public string name;
        public string stages;
        public int participants;
    
        public string GetDataUID()
        {
            return name;
        }
    }
    
    [CreateAssetMenu]
    public class TournamentFormatBalanceData : StaticDataReferenceBalanceData<
        TournamentFormatDataSource, 
        TournamentFormat, 
        TournamentFormatBalanceObject,
        TournamentFormat, 
        TournamentFormatBalanceObject>
    {
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