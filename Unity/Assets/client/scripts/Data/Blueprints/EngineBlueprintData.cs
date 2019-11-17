using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using UnityEngine;

[CreateAssetMenu(menuName = "Blueprints/Engine")]
public class EngineBlueprintData : PhysicalComponentBlueprintData<JunkyardDogs.Components.Engine, JunkyardDogs.Specifications.Engine>, IStaticDataBalance<EngineBlueprintBalanceObject>
{
    public void ApplyBalance(EngineBlueprintBalanceObject balance)
    {
        name = balance.name;
        _specification = new SpecificationStaticDataReference();
        _specification.ID = balance.specification;
        _manufacturer = new ManufacturerStaticDataReference();
        _manufacturer.ID = balance.manufacturer;
        Material = new MaterialStaticDataReference();
        Material.ID = balance.material;
    }

    public EngineBlueprintBalanceObject GetBalance()
    {
        EngineBlueprintBalanceObject balance = new EngineBlueprintBalanceObject();
        balance.name = name;
        balance.specification = _specification == null ? string.Empty : _specification.ID;
        balance.manufacturer = _manufacturer == null ? string.Empty : _manufacturer.ID;
        return balance;
    }
}