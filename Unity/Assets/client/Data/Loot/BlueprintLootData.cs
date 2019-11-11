using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using UnityEngine;

[CreateAssetMenu]
public class BlueprintLootData : LootData<BlueprintDataBase>, IStaticDataBalance<BlueprintLootBalanceObject>
{
    [SerializeField, BlueprintStaticDataReference] 
    private BlueprintStaticDataReference _blueprint;
    
    public override ILoot GetLoot(ILootDataModel dataModel)
    {
        return _blueprint.Data;
    }

    public void ApplyBalance(BlueprintLootBalanceObject balance)
    {
        _blueprint = new BlueprintStaticDataReference();
        _blueprint.ID = balance.blueprintId;
    }

    public BlueprintLootBalanceObject GetBalance()
    {
        throw new System.NotImplementedException();
    }
}