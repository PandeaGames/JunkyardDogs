using JunkyardDogs.Components;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using UnityEngine;

public abstract class PhysicalComponentBlueprintData<TGeneratedData> : ComponentBlueprintData<TGeneratedData> where TGeneratedData:PhysicalComponent
{
    [SerializeField, MaterialStaticDataReference]
    public MaterialStaticDataReference Material;
    
    public override TGeneratedData DoGenerate(int seed)
    {
        return ComponentUtils.GenerateComponent(_specification, _manufacturer, Material) as TGeneratedData;
    }
}