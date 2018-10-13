using UnityEngine;
using JunkyardDogs.Specifications;
using System;
using JunkyardDogs.Components;
using Component = JunkyardDogs.Components.Component;
using WeakReference = Data.WeakReference;
using Weapon = JunkyardDogs.Specifications.Weapon;

[Serializable]
public class WeaponProcessorBlueprint : PhysicalComponentBlueprint<WeaponProcessorBlueprintData>
{
    [Header("Weapon")]
    [SerializeField] 
    private WeaponBlueprint _weapon;
    
    protected override void DoGenerate(int seed, Action<Component> onComplete, Action onError)
    {
        base.DoGenerate(seed, (component) =>
        {
            WeaponProcessor processor = component as WeaponProcessor;
            
            _weapon.Generate(seed, weapon =>
            {
                processor.Weapon = weapon as JunkyardDogs.Components.Weapon;
                onComplete(processor);
            }, onError);
            
        }, onError);
    }
}