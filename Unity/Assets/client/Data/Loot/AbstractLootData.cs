using UnityEngine;

public interface ILootData<TUser>
{
    void GrantLoot(TUser user);
}

public abstract class AbstractLootData : AbstractStaticData
{
    public abstract ILoot GetLoot(ILootDataModel dataModel);
}
