using UnityEngine;
using JunkyardDogs.Specifications;
using System;
using JunkyardDogs.Components;
using JunkyardDogs.Data;
using Component = JunkyardDogs.Components.Component;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

[Serializable]
public abstract class PhysicalComponentBlueprint<T> : ComponentBlueprint<T> where T:PhysicalComponentBlueprintData
{
    [SerializeField]
    private MaterialStaticDataReference _material;

    protected override Component DoGenerate(int seed)
    {
        PhysicalComponent physicalComponent = base.DoGenerate(seed) as PhysicalComponent;
        physicalComponent.Material = _material;
        return physicalComponent;
    }
}