using System;
using JunkyardDogs.Data.Balance;
using UnityEngine;


[Serializable]
public struct BreakpointBalanceObject:IStaticDataBalanceObject
{
    public string name;
    
    public int break_01;
    public int break_02;
    public int break_03;
    public int break_04;
    public int break_05;
    public int break_06;
    public int break_07;
    public int break_08;
    public int break_09;
    public int break_10;
    public int break_11;
    public int break_12;
    public int break_13;
    public int break_14;
    public int break_15;
    public int break_16;
    public int break_17;
    public int break_18;
    public int break_19;
    public int break_20;
    public int break_21;
    public int break_22;
    public int break_23;
    public int break_24;
    public int break_25;
    
    public string GetDataUID()
    {
        return name;
    }
}

[CreateAssetMenu(menuName = MENU_NAME)]
public class BreakpointBalanceData : StaticDataReferenceBalanceData<
    BreakpointDataSource, 
    BreakpointData,
    BreakpointData,
    BreakpointBalanceObject>
{
    private const string MENU_NAME =  BalanceDataUtilites.BALANCE_MENU_FOLDER + "BreakpointBalanceData";

    public const string DATA_PATH = "Assets/AssetBundles/Data/Breakpoints/";
    
    public override string GetUIDFieldName()
    {
        return "name";
    }

    protected override string GetNewDataFolder()
    {
        return DATA_PATH;
    }
}