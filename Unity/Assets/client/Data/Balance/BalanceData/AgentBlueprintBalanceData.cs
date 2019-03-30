
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
[CreateAssetMenu]
public class AgentBlueprintBalanceData : StaticDataReferenceBalanceData<
    BlueprintDataSource, 
    BlueprintDataBase, 
    BlueprintBalanceObject,
    AgentBlueprintData, 
    AgentBlueprintBalanceObject>
{
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
