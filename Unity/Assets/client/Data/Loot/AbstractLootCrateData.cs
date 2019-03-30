using UnityEngine;

public abstract class AbstractLootCrateData : ScriptableObject
{
    public abstract ILoot[] GetLoot();
}
