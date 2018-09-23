using UnityEngine;

[CreateAssetMenu(menuName = "Blueprints/WeaponProcessorBlueprintData")]
public class WeaponProcessorBlueprintData : PhysicalComponentBlueprintData
{
    [SerializeField]
    private WeaponProcessorBlueprint _blueprint;
    
    public override BlueprintBase GetBlueprint()
    {
        return _blueprint;
    }
}