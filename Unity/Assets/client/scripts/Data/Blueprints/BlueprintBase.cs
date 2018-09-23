using UnityEngine;
using System;

[Serializable]
public abstract class BlueprintBase
{
    [SerializeField]
    protected BlueprintData _blueprintBase;

    public abstract bool isValidData(BlueprintData data);

    public abstract void GenerateObject(Action<System.Object> onComplete, Action onError);
    public abstract void GenerateObject(int seed, Action<System.Object> onComplete, Action onError);
}