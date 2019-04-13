
using System;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using UnityEngine;

[Serializable]
public struct EliminationFormatBalanceObject:IStaticDataBalanceObject
{
    public string name;
    public int groups;
    public int eliminations;
    
    public string GetDataUID()
    {
        return name;
    }
}

[CreateAssetMenu(menuName = MENU_NAME)]
public class EliminationFormatBalanceData : StaticDataReferenceBalanceData<
    StageFormatDataSource, 
    StageFormat,
    EliminationFormat, 
    EliminationFormatBalanceObject>
{
    private const string MENU_NAME =  BalanceDataUtilites.BALANCE_MENU_FOLDER + "EliminationFormatBalanceData";

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
