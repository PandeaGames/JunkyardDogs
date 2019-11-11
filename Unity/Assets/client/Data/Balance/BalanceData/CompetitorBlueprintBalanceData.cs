using System;
using System.Collections.Generic;
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
    public uint level;
    [SerializeField]
    public string bot_01;
    [SerializeField]
    public string bot_02;
    [SerializeField]
    public string bot_03;
    [SerializeField]
    public string bot_04;
    [SerializeField]
    public string bot_05;
    public string GetDataUID()
    {
        return name;
    }

    public IEnumerable<string> bots
    {
        get
        {
            if (bot_01 != string.Empty)
            {
                yield return bot_01;
            }
            if (bot_02 != string.Empty)
            {
                yield return bot_02;
            }
            if (bot_03 != string.Empty)
            {
                yield return bot_03;
            }
            if (bot_04 != string.Empty)
            {
                yield return bot_04;
            }
            if (bot_05 != string.Empty)
            {
                yield return bot_05;
            }
        }
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