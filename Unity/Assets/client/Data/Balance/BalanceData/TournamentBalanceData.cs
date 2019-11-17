
using System;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using UnityEngine;

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
