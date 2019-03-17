using System;

[Serializable]
public abstract class Blueprint<T, K> : BlueprintBase where K:BlueprintData
{
    public override bool isValidData(BlueprintData data)
    {
        return data is K;
    }
    
    protected T DoGenerate()
    {
        return DoGenerate(0);
    }
    
    protected abstract T DoGenerate(int seed);
    
    public T Generate()
    {
        return Generate(0);
    }

    public override System.Object GenerateObject()
    {
        return Generate();
    }
    
    public override System.Object GenerateObject(int seed)
    {
        return Generate(seed);
    }
    
    public T Generate(int seed)
    {
        if (_blueprintBase)
        {
            return (T)_blueprintBase.GetBlueprint().GenerateObject(seed);
        }
        else
        {
            return DoGenerate(seed);
        }
    }
}