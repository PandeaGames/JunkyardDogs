using UnityEngine;

[CreateAssetMenu(menuName = "Blueprints/Motherboard Blueprint")]
public class MotherboardBlueprintData : BlueprintData
{
    [SerializeField]
    private MotherboardBlueprint _blueprint;
    
    public override BlueprintBase GetBlueprint()
    {
        return _blueprint;
    }
}