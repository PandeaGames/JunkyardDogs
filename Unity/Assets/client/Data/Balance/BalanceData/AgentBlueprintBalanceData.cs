
using System;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using JunkyardDogs.Specifications;
using UnityEngine;
using Material = JunkyardDogs.Specifications.Material;

[Serializable]
public struct AgentBlueprintBalanceObject:IStaticDataBalanceObject
{
    public string name;
    public string directiveActions;
    public string state;
    
    public string GetDataUID()
    {
        return name;
    }
}
[CreateAssetMenu(menuName = MENU_NAME)]
public class AgentBlueprintBalanceData : StaticDataReferenceBalanceData<
    BlueprintDataSource, 
    BlueprintDataBase,
    AgentBlueprintData, 
    AgentBlueprintBalanceObject>
{
    private const string MENU_NAME =  BalanceDataUtilites.BALANCE_MENU_FOLDER + "AgentBlueprintBalanceData";
    public const string DATA_PATH = "Assets/AssetBundles/Data/Blueprints/";
    
    protected override string GetNewDataFolder()
    {
        return DATA_PATH;
    }

    public override string GetUIDFieldName()
    {
        return "name";
    }
}
