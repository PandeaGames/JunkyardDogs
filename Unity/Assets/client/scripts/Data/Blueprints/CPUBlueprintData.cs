using JunkyardDogs.Components;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using UnityEngine;

[CreateAssetMenu(menuName = "Blueprints/Plate")]
public class CPUBlueprintData : ComponentBlueprintData<CPU>, IStaticDataBalance<CPUBlueprintBalanceObject>
{
    public void ApplyBalance(CPUBlueprintBalanceObject balance)
    {
        name = balance.name;
        _specification = new SpecificationStaticDataReference();
        _specification.ID = balance.specification;
        _manufacturer = new ManufacturerStaticDataReference();
        _manufacturer.ID = balance.manufacturer;
    }

    public CPUBlueprintBalanceObject GetBalance()
    {
        CPUBlueprintBalanceObject balance = new CPUBlueprintBalanceObject();
        balance.name = name;
        balance.specification = _specification == null ? string.Empty : _specification.ID;
        balance.manufacturer = _manufacturer == null ? string.Empty : _manufacturer.ID;
        return balance;
    }
}