using UnityEngine;
using System;

[Serializable]
public abstract class BlueprintBase
{
    [SerializeField]
    protected BlueprintData _blueprintBase;

    public abstract bool isValidData(BlueprintData data);

    public abstract System.Object GenerateObject();
    public abstract System.Object GenerateObject(int seed);
}