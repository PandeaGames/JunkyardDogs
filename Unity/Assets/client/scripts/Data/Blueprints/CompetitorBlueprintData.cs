
using UnityEngine;

[CreateAssetMenu(menuName = "Blueprints/CompetitorBlueprint")]
public class CompetitorBlueprintData : BlueprintData
{
    [SerializeField]
    private CompetitorBlueprint _blueprint;
    
    public override BlueprintBase GetBlueprint()
    {
        return _blueprint;
    }
}
