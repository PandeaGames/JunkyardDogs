using JunkyardDogs.Components;
using JunkyardDogs.Data;
using UnityEngine;

[CreateAssetMenu(menuName = "Blueprints/WeaponProcessorBlueprintData")]
public class WeaponProcessorBlueprintData : PhysicalComponentBlueprintData<WeaponProcessor>
{
    [Header("Weapon")]
    [SerializeField, WeaponBlueprintStaticDataReference] 
    private WeaponBlueprintStaticDataReference _weapon;
    
    public override WeaponProcessor DoGenerate(int seed)
    {
        WeaponProcessor processor = new WeaponProcessor();
        Weapon weapon = _weapon.Data.DoGenerate(seed);
        processor.Weapon = weapon;

        return processor;
    }
}