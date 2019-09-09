using System;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using JunkyardDogs.Specifications;
using UnityEngine;

[Serializable]
public class EngineBalanceObject:PhysicalSpecificationBalanceObject
{
    public float acceleration;
    public float maxSpeed;
}

[CreateAssetMenu(menuName = MENU_NAME)]
public class EngineBalanceData : StaticDataReferenceBalanceData<
    SpecificationDataSource, 
    Specification,
    Engine,
    EngineBalanceObject>
{
    private const string MENU_NAME =  BalanceDataUtilites.BALANCE_MENU_FOLDER + "EngineBalanceData";

    public const string NEW_DATA_PATH = "Assets/AssetBundles/Data/Products/Engines/";
    
    public override string GetUIDFieldName()
    {
        return "name";
    }

    protected override string GetNewDataFolder()
    {
        return NEW_DATA_PATH;
    }
}
