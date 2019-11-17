using JunkyardDogs.Components;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using UnityEngine;

[CreateAssetMenu(menuName = "Blueprints/WeaponBlueprintData")]
public class WeaponBlueprintData : PhysicalComponentBlueprintData<Weapon, JunkyardDogs.Specifications.Weapon>, IStaticDataBalance<WeaponBlueprintBalanceObject>
{
    public void ApplyBalance(WeaponBlueprintBalanceObject balance)
    {
        name = balance.name;
        _specification = new SpecificationStaticDataReference();
        _specification.ID = balance.specification;
        _manufacturer = new ManufacturerStaticDataReference();
        _manufacturer.ID = balance.manufacturer;
        Material = new MaterialStaticDataReference();
        Material.ID = balance.material;
    }

    public WeaponBlueprintBalanceObject GetBalance()
    {
        WeaponBlueprintBalanceObject balance = new WeaponBlueprintBalanceObject();
        balance.name = name;
        balance.specification = _specification == null ? string.Empty : _specification.ID;
        balance.manufacturer = _manufacturer == null ? string.Empty : _manufacturer.ID;
        return balance;
    }
}