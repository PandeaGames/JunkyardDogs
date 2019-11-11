using UnityEngine;

public abstract class AbstractLootCrateData : AbstractStaticData
{
    public abstract ILoot[] GetLoot(ILootDataModel dataModel);
}
