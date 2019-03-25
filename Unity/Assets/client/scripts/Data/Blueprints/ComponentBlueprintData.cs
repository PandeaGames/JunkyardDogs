using UnityEngine;
using JunkyardDogs.Specifications;
using System;
using JunkyardDogs.Components;
using JunkyardDogs.Data;
using Component = JunkyardDogs.Components.Component;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

[CreateAssetMenu(menuName = "Blueprints/Component Blueprint")]
public abstract class ComponentBlueprintData<TGeneratedData> : BlueprintData<TGeneratedData> where TGeneratedData:Component
{
    [StaticDataReference(path:SpecificationDataProvider.FULL_PATH),SerializeField]
    protected SpecificationStaticDataReference _specification;
    
    [SerializeField, StaticDataReference(path:ManufacturerDataProvider.FULL_PATH)]
    protected ManufacturerStaticDataReference _manufacturer;
        
    public override TGeneratedData DoGenerate(int seed)
    {
        return ComponentUtils.GenerateComponent(_specification, _manufacturer) as TGeneratedData;
    }
}