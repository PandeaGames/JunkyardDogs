
using JunkyardDogs.Components;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;

public class DirectiveBlueprintData : ComponentBlueprintData<Directive, JunkyardDogs.Specifications.Directive>, IStaticDataBalance<DirectiveBlueprintBalanceObject>
{
    public void ApplyBalance(DirectiveBlueprintBalanceObject balance)
    {
        name = balance.name;
        _specification = new SpecificationStaticDataReference();
        _specification.ID = balance.specification;
        _manufacturer = new ManufacturerStaticDataReference();
        _manufacturer.ID = balance.manufacturer;
    }

    public DirectiveBlueprintBalanceObject GetBalance()
    {
        DirectiveBlueprintBalanceObject balance = new DirectiveBlueprintBalanceObject();
        balance.name = name;
        balance.specification = _specification == null ? string.Empty : _specification.ID;
        balance.manufacturer = _manufacturer == null ? string.Empty : _manufacturer.ID;
        return balance;
    }
}
