using JunkyardDogs.Components;
using UnityEngine;

[CreateAssetMenu(menuName = "Blueprints/WeaponProcessorBlueprintData")]
public class WeaponProcessorBlueprintData : PhysicalComponentBlueprintData<WeaponProcessor>
{
    [Header("Weapon")]
    [SerializeField] 
    private WeaponBlueprintData _weapon;
    
    public override WeaponProcessor DoGenerate(int seed)
    {
        WeaponProcessor processor = new WeaponProcessor();
        Weapon weapon = _weapon.DoGenerate(seed);
        processor.Weapon = weapon;

        return processor;
    }
}