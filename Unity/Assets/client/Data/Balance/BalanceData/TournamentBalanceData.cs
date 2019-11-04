
using System;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using UnityEngine;

[Serializable]
public struct TournamentBalanceObject:IStaticDataBalanceObject
{
    public string name;
    public string format;
    public string nation;
    public int roundPaceSeconds;
    public int seasonDelaySeconds;
    public string unlockNation;
    public string unlockCriteria;
    public int nationalExpUnlockBreakpoint;
    public int expUnlockBreakpoint;
    public string lootCrateRewards;

    public string participant01;
    public string participant02;
    public string participant03;
    public string participant04;
    public string participant05;
    public string participant06;
    public string participant07;
    public string participant08;
    public string participant09;
    public string participant10;
    public string participant11;
    public string participant12;
    public string participant13;
    public string participant14;
    public string participant15;
    public string participant16;
    public string participant17;
    public string participant18;
    public string participant19;
    public string participant20;
    public string participant21;
    public string participant22;
    public string participant23;
    public string participant24;
    public string participant25;
    public string participant26;
    public string participant27;
    public string participant28;
    public string participant29;
    public string participant30;
    public string participant31;
    
    public string GetDataUID()
    {
        return name;
    }
}

[CreateAssetMenu(menuName = MENU_NAME)]
public class TournamentBalanceData : StaticDataReferenceBalanceData<
    TournamentDataSource, 
    Tournament,
    Tournament, 
    TournamentBalanceObject>
{
    private const string MENU_NAME =  BalanceDataUtilites.BALANCE_MENU_FOLDER + "TournamentBalanceData";

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
