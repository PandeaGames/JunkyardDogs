using System;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using UnityEngine;

[Serializable]
public struct CompetitorBalanceObject:IStaticDataBalanceObject
{
    [SerializeField]
    public string name;
    [SerializeField]
    public string nationality;
    [SerializeField]
    public string bots;
    public string GetDataUID()
    {
        return name;
    }
}

[CreateAssetMenu(menuName = MENU_NAME)]
public class CompetitorBlueprintBalanceData : StaticDataReferenceBalanceData<
    BlueprintDataSource, 
    BlueprintDataBase,
    CompetitorBlueprintData, 
    CompetitorBalanceObject>
{
    private const string MENU_NAME =  BalanceDataUtilites.BALANCE_MENU_FOLDER + "CompetitorBlueprintBalanceData";

    
    public override string GetUIDFieldName()
    {
        return "name";
    }

    protected override string GetNewDataFolder()
    {
        return BlueprintBalanceData.DATA_PATH;
    }
}