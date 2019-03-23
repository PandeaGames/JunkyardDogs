using JunkyardDogs.Components;
using UnityEngine;

[CreateAssetMenu(menuName = "Blueprints/WeaponBlueprintData")]
public class WeaponBlueprintData : PhysicalComponentBlueprintData<Weapon>
{
    public override Weapon DoGenerate(int seed)
    {
        throw new System.NotImplementedException();
    }
}