﻿
using JunkyardDogs.Components;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using UnityEngine;

[CreateAssetMenu(menuName = "Blueprints/Plate")]
public class PlateBlueprintData : PhysicalComponentBlueprintData<Plate, JunkyardDogs.Specifications.Plate>, IStaticDataBalance<PlateBlueprintBalanceObject>
{
    public void ApplyBalance(PlateBlueprintBalanceObject balance)
    {
        name = balance.name;
        _specification = new SpecificationStaticDataReference();
        _specification.ID = balance.specification;
        _manufacturer = new ManufacturerStaticDataReference();
        _manufacturer.ID = balance.manufacturer;
        Material = new MaterialStaticDataReference();
        Material.ID = balance.material;
    }

    public PlateBlueprintBalanceObject GetBalance()
    {
        PlateBlueprintBalanceObject balance = new PlateBlueprintBalanceObject();
        balance.name = name;
        balance.specification = _specification == null ? string.Empty : _specification.ID;
        balance.manufacturer = _manufacturer == null ? string.Empty : _manufacturer.ID;
        return balance;
    }
}