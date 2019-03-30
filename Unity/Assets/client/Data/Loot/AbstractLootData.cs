using UnityEngine;

public interface ILootData<TUser>
{
    void GrantLoot(TUser user);
}

public abstract class AbstractLootData : ScriptableObject
{
    public abstract ILoot GetLoot();
}
