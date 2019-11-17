using JunkyardDogs.Components;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using UnityEngine;

[CreateAssetMenu(menuName = "Blueprints/WeaponProcessorBlueprintData")]
public class WeaponProcessorBlueprintData : ComponentBlueprintData<WeaponProcessor, JunkyardDogs.Specifications.WeaponChip>, IStaticDataBalance<WeaponProcessorBlueprintBalanceObject>
{
    [Header("Weapon")]
    [SerializeField, WeaponBlueprintStaticDataReference] 
    private WeaponBlueprintStaticDataReference _weapon;
    
    public override WeaponProcessor DoGenerate(int seed)
    {
        WeaponProcessor processor = base.DoGenerate(seed);

        if (_weapon != null && _weapon.Data != null)
        {
            Weapon weapon = _weapon.Data.DoGenerate(seed);
            processor.Weapon = weapon;
        }
        
        return processor;
    }

    public void ApplyBalance(WeaponProcessorBlueprintBalanceObject balance)
    {
        _weapon = new WeaponBlueprintStaticDataReference();
        _weapon.ID = balance.weapon;
        name = balance.name;
        _specification = new SpecificationStaticDataReference();
        _specification.ID = balance.specification;
        _manufacturer = new ManufacturerStaticDataReference();
        _manufacturer.ID = balance.manufacturer;
    }

    public WeaponProcessorBlueprintBalanceObject GetBalance()
    {
        WeaponProcessorBlueprintBalanceObject balance = new WeaponProcessorBlueprintBalanceObject();
        balance.name = name;
        balance.weapon = _weapon == null ? string.Empty : _weapon.ID;
        balance.specification = _specification == null ? string.Empty : _specification.ID;
        balance.manufacturer = _manufacturer == null ? string.Empty : _manufacturer.ID;
        return balance;
    }
}