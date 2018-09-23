using UnityEngine;

[CreateAssetMenu(menuName = "Blueprints/WeaponBlueprintData")]
public class WeaponBlueprintData : PhysicalComponentBlueprintData
{
    [SerializeField]
    private WeaponBlueprint _blueprint;
    
    public override BlueprintBase GetBlueprint()
    {
        return _blueprint;
    }
}