using System;
using JunkyardDogs.Data.Balance;
using UnityEngine;


[Serializable]
public struct BreakpointBalanceObject:IStaticDataBalanceObject
{
    public string name;
    
    public double break_01;
    public double break_02;
    public double break_03;
    public double break_04;
    public double break_05;
    public double break_06;
    public double break_07;
    public double break_08;
    public double break_09;
    public double break_10;
    public double break_11;
    public double break_12;
    public double break_13;
    public double break_14;
    public double break_15;
    public double break_16;
    public double break_17;
    public double break_18;
    public double break_19;
    public double break_20;
    public double break_21;
    public double break_22;
    public double break_23;
    public double break_24;
    public double break_25;
    
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