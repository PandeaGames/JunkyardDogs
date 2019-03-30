using JunkyardDogs.Data;
using UnityEngine;

[CreateAssetMenu]
public class BlueprintLootData : LootData<BlueprintDataBase>
{
    [SerializeField, BlueprintStaticDataReference] 
    private BlueprintStaticDataReference _blueprint;
    
    public override ILoot GetLoot()
    {
        return _blueprint.Data;
    }
}