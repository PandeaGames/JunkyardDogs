using UnityEngine;

[CreateAssetMenu(menuName = "Blueprints/ChassisBlueprint")]
public class ChassisBlueprintData : PhysicalComponentBlueprintData
{
    [SerializeField]
    private ChassisBlueprint _blueprint;
    
    public override BlueprintBase GetBlueprint()
    {
        return _blueprint;
    }
}