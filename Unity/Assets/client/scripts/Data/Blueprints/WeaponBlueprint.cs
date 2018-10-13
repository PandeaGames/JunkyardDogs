using UnityEngine;
using JunkyardDogs.Specifications;
using System;
using JunkyardDogs.Components;
using Component = JunkyardDogs.Components.Component;
using WeakReference = Data.WeakReference;
using Weapon = JunkyardDogs.Specifications.Weapon;

[Serializable]
public class WeaponBlueprint : PhysicalComponentBlueprint<WeaponBlueprintData>
{
}