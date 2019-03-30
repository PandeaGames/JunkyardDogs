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

[CreateAssetMenu]
public class CompetitorBlueprintBalanceData : StaticDataReferenceBalanceData<
    BlueprintDataSource, 
    BlueprintDataBase, 
    BlueprintBalanceObject,
    CompetitorBlueprintData, 
    CompetitorBalanceObject>
{
    public override string GetUIDFieldName()
    {
        return "name";
    }

    protected override string GetNewDataFolder()
    {
        return BlueprintBalanceData.DATA_PATH;
    }
}