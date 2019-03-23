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
    private SpecificationStaticDataReference _specification;
    
    [SerializeField][WeakReference(typeof(Manufacturer))]
    private WeakReference _manufacturer;
        
    public override TGeneratedData DoGenerate(int seed)
    {
        return ComponentUtils.GenerateComponent(_specification) as TGeneratedData;
    }
}