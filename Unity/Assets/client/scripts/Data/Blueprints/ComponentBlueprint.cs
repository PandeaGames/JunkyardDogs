using UnityEngine;
using JunkyardDogs.Specifications;
using System;
using JunkyardDogs.Components;
using JunkyardDogs.Data;
using PandeaGames.Data.Static;

using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;
using Component = JunkyardDogs.Components.Component;

[Serializable]
public class ComponentBlueprint<T> : Blueprint<Component, T> where T:BlueprintData
{   
    /*[StaticDataReference(path:SpecificationDataProvider.FULL_PATH)]*/[SerializeField]
    private SpecificationStaticDataReference _specification;
    
    [SerializeField][WeakReference(typeof(Manufacturer))]
    private WeakReference _manufacturer;
        
    protected override Component DoGenerate(int seed)
    {
        return ComponentUtils.GenerateComponent(_specification);
    }
}