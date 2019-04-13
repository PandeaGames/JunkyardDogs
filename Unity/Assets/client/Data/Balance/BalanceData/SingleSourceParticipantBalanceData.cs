using System;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using UnityEngine;

[Serializable]
public struct SingleSourceParticipantBalanceObject:IStaticDataBalanceObject
{
    public string name;
    public string competitor;
    public int botIndex;
        
    public string GetDataUID()
    {
        return name;
    }
}
    
[CreateAssetMenu(menuName = MENU_NAME)]
public class SingleSourceParticipantBalanceData : StaticDataReferenceBalanceData<
    ParticipantDataSource, 
    ParticipantData,
    SingleSourceParticipantData, 
    SingleSourceParticipantBalanceObject>
{
    private const string MENU_NAME =  BalanceDataUtilites.BALANCE_MENU_FOLDER + "SingleSourceParticipantBalanceData";

    public const string NATION_DATA_PATH = "Assets/AssetBundles/Data/Competitors/";
        
    public override string GetUIDFieldName()
    {
        return "name";
    }

    protected override string GetNewDataFolder()
    {
        return NATION_DATA_PATH;
    }
}