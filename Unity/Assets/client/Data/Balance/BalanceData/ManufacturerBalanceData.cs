
using System;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using JunkyardDogs.Specifications;
using UnityEngine;
using Material = JunkyardDogs.Specifications.Material;

[Serializable]
public struct ManufacturerBalanceObject:IStaticDataBalanceObject
{
    public string name;
    public string nationality;
    
    public string distinctionId_01;
    public int distinctionValue_01;
    public string distinctionId_02;
    public int distinctionValue_02;
    public string distinctionId_03;
    public int distinctionValue_03;
    public string distinctionId_04;
    public int distinctionValue_04;
    public string distinctionId_05;
    public int distinctionValue_05;
    
    public string GetDataUID()
    {
        return name;
    }
}
[CreateAssetMenu(menuName = MENU_NAME)]
public class ManufacturerBalanceData : StaticDataReferenceBalanceData<
    ManufactuererDataSource,
    Manufacturer,
    Manufacturer, 
    ManufacturerBalanceObject>
{   
    private const string MENU_NAME =  BalanceDataUtilites.BALANCE_MENU_FOLDER + "LootCrateBalanceData";

    
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
