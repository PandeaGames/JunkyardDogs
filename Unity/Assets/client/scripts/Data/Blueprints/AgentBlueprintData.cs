using UnityEngine;

[CreateAssetMenu(menuName = "Blueprints/Agent Blueprint")]
public class AgentBlueprintData : BlueprintData
{
    [SerializeField]
    private AgentBlueprint _blueprint;

    public override BlueprintBase GetBlueprint()
    {
        return _blueprint;
    }
}