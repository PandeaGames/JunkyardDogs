using UnityEngine;

[CreateAssetMenu(menuName = "Blueprints/BotBlueprint")]
public class BotBlueprintData : BlueprintData
{
    [SerializeField]
    private BotBlueprint _blueprint;
    
    public override BlueprintBase GetBlueprint()
    {
        return _blueprint;
    }
}