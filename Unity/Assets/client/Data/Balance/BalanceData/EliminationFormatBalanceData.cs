
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

[CreateAssetMenu]
public class EliminationFormatBalanceData : StaticDataReferenceBalanceData<
    StageFormatDataSource, 
    StageFormat, 
    EliminationFormatBalanceObject,
    EliminationFormat, 
    EliminationFormatBalanceObject>
{
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
