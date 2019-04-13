
using System;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using JunkyardDogs.Specifications;
using UnityEngine;
using Material = JunkyardDogs.Specifications.Material;

[Serializable]
public class SpecificationBalanceObject:IStaticDataBalanceObject
{
    public string name;
    public string GetDataUID()
    {
        return name;
    }
}
[CreateAssetMenu(menuName = MENU_NAME)]
public class SpecificationBalanceData : StaticDataReferenceBalanceData<
    SpecificationDataSource, 
    Specification, 
    Specification,
    SpecificationBalanceObject>
{
    private const string MENU_NAME =  BalanceDataUtilites.BALANCE_MENU_FOLDER + "PlateBlueprintBalanceData";

    public const string DATA_PATH = "Assets/AssetBundles/Data/Products/";
    
    protected override string GetNewDataFolder()
    {
        return DATA_PATH;
    }

    public override string GetUIDFieldName()
    {
        return "name";
    }
}
