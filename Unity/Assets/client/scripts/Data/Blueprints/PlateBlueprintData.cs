
using UnityEngine;

public class PlateBlueprintData : PhysicalComponentBlueprintData
{
    [SerializeField]
    private PlateBlueprint _blueprint;
    
    public override BlueprintBase GetBlueprint()
    {
        return _blueprint;
    }
}
