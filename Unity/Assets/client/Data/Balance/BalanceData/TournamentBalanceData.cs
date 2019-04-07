
using System;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using UnityEngine;

[Serializable]
public struct TournamentBalanceObject:IStaticDataBalanceObject
{
    public string name;
    public string format;
    public string participants;
    public int roundPaceSeconds;
    public int seasonDelaySeconds;
    public string lootCrateRewards;
    
    public string GetDataUID()
    {
        return name;
    }
}

[CreateAssetMenu]
public class TournamentBalanceData : StaticDataReferenceBalanceData<
    TournamentDataSource, 
    Tournament, 
    TournamentBalanceObject,
    Tournament, 
    TournamentBalanceObject>
{
    public const string DATA_PATH = "Assets/AssetBundles/Data/Tournaments/Tournaments/";

    public override string GetUIDFieldName()
    {
        return "name";
    }

    protected override string GetNewDataFolder()
    {
        return DATA_PATH;
    }
}
