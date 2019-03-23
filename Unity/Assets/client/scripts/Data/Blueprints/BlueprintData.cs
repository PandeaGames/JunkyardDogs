using UnityEngine;

public abstract class BlueprintData<TGeneratedData> : ScriptableObject
{
    public TGeneratedData DoGenerate()
    {
        return DoGenerate(0);
    }
    
    public abstract TGeneratedData DoGenerate(int seed);
}