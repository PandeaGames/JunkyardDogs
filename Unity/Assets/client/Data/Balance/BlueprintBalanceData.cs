

using System;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using UnityEngine;

[Serializable]
public struct BlueprintBalanceObject:IStaticDataBalanceObject
{
    public string name;
    public string GetDataUID()
    {
        return name;
    }
}

[CreateAssetMenu]
public class BlueprintBalanceData : StaticDataReferenceBalanceData<
    BlueprintDataSource, 
    BlueprintDataBase, 
    BlueprintBalanceObject,
    BlueprintDataBase, 
    BlueprintBalanceObject>
{
    public const string DATA_PATH = "Assets/AssetBundles/Data/Blueprints";
    
    public override string GetUIDFieldName()
    {
        return "name";
    }

    protected override string GetNewDataFolder()
    {
        return DATA_PATH;
    }
}
