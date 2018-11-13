using UnityEngine;
using JunkyardDogs.Specifications;
using System;
using JunkyardDogs.Components;
using WeakReference = Data.WeakReference;
using Component = JunkyardDogs.Components.Component;

[Serializable]
public class ComponentBlueprint<T> : Blueprint<Component, T> where T:BlueprintData
{
    [SerializeField][WeakReference(typeof(Specification))]
    private WeakReference _specification;
            
    [SerializeField][WeakReference(typeof(Manufacturer))]
    private WeakReference _manufacturer;
        
    protected override void DoGenerate(int seed, Action<Component> onComplete, Action onError)
    {
        ComponentUtils.GenerateComponent(_specification, (comp) =>
        {
            _manufacturer.LoadAssetAsync<Manufacturer>((manufacturer, manufacturerReference) =>
                {
                    comp.Manufacturer = _manufacturer;
                    onComplete(comp);
                }, (e) => onError());
        });
    }
}