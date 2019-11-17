using System;
using UnityEngine;
using JunkyardDogs.Components;
using JunkyardDogs.Data;
using JunkyardDogs.Specifications;

[CreateAssetMenu(menuName = "Blueprints/Component Blueprint")]
public abstract class ComponentBlueprintData<TGeneratedData, TSpecification> : BlueprintData<TGeneratedData> where TSpecification:Specification where TGeneratedData:Component<TSpecification>
{
    [StaticDataReference(path:SpecificationDataProvider.FULL_PATH),SerializeField]
    protected SpecificationStaticDataReference _specification;
    
    [SerializeField, StaticDataReference(path:ManufacturerDataProvider.FULL_PATH)]
    protected ManufacturerStaticDataReference _manufacturer;
        
    public override TGeneratedData DoGenerate(int seed)
    {
        string generateDataName = typeof(TGeneratedData).ToString();
        System.Object generatedData = ComponentUtils.GenerateComponent(_specification, _manufacturer);
        bool isGeneratedType = generatedData is TGeneratedData;
        try
        {
            return (TGeneratedData) generatedData;
        }
        catch (InvalidCastException exception)
        {
            throw exception;
        }

        return null;
    }
}