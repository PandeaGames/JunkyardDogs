using UnityEngine;
using JunkyardDogs.Specifications;
using System;
using JunkyardDogs.Components;
using Component = JunkyardDogs.Components.Component;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;
using Weapon = JunkyardDogs.Specifications.Weapon;

[Serializable]
public class WeaponProcessorBlueprint : PhysicalComponentBlueprint<WeaponProcessorBlueprintData>
{
    [Header("Weapon")]
    [SerializeField] 
    private WeaponBlueprint _weapon;
    
    protected override Component DoGenerate(int seed)
    {
        Component component = base.DoGenerate(seed);
        
        WeaponProcessor processor = component as WeaponProcessor;
        JunkyardDogs.Components.Weapon weapon = _weapon.Generate(seed) as JunkyardDogs.Components.Weapon;
        processor.Weapon = weapon;

        return component;
    }
}