using System;

[Serializable]
public abstract class Blueprint<T, K> : BlueprintBase where K:BlueprintData
{
    public override bool isValidData(BlueprintData data)
    {
        return data is K;
    }
    
    protected void DoGenerate(Action<T> onComplete, Action onError)
    {
        DoGenerate(0, onComplete, onError);
    }
    
    protected abstract void DoGenerate(int seed, Action<T> onComplete, Action onError);
    
    public void Generate(Action<T> onComplete, Action onError)
    {
        Generate(0, onComplete, onError);
    }

    public override void GenerateObject(Action<System.Object> onComplete, Action onError)
    {
        Generate((obj) => { onComplete(obj); },onError);
    }
    
    public override void GenerateObject(int seed,Action<System.Object> onComplete, Action onError)
    {
        Generate(seed, (obj) => { onComplete(obj); },onError);
    }
    
    public void Generate(int seed, Action<T> onComplete, Action onError)
    {
        if (_blueprintBase)
        {
            _blueprintBase.GetBlueprint().GenerateObject(seed, (obj) => { onComplete((T)obj); }, onError);
        }
        else
        {
            DoGenerate(seed, onComplete, onError);
        }
    }
}