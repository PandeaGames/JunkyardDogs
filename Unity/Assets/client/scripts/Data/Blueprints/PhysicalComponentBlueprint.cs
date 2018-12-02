using UnityEngine;
using JunkyardDogs.Specifications;
using System;
using JunkyardDogs.Components;
using Component = JunkyardDogs.Components.Component;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

[Serializable]
public abstract class PhysicalComponentBlueprint<T> : ComponentBlueprint<T> where T:PhysicalComponentBlueprintData
{
    [SerializeField][WeakReference(typeof(JunkyardDogs.Specifications.Material))]
    private WeakReference _material;

    protected override void DoGenerate(int seed, Action<Component> onComplete, Action onError)
    {
        base.DoGenerate(seed, (component) =>
        {
            PhysicalComponent physicalComponent = component as PhysicalComponent;
            physicalComponent.Material = _material;
            onComplete(physicalComponent);
        }, () => { });
    }
}