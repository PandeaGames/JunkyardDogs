using JunkyardDogs.Components;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using JunkyardDogs.Specifications;
using UnityEngine;

public abstract class PhysicalComponentBlueprintData<TGeneratedData, TSpecification> : ComponentBlueprintData<TGeneratedData,TSpecification> where TGeneratedData:PhysicalComponent<TSpecification> where TSpecification:PhysicalSpecification
{
    [SerializeField, MaterialStaticDataReference]
    public MaterialStaticDataReference Material;
    
    public override TGeneratedData DoGenerate(int seed)
    {
        return ComponentUtils.GenerateComponent(_specification, _manufacturer, Material) as TGeneratedData;
    }
}