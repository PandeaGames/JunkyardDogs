using UnityEngine;
using JunkyardDogs.Specifications;
using System;
using JunkyardDogs.Components;
using UnityEditorInternal;
using Component = JunkyardDogs.Components.Component;
using WeakReference = Data.WeakReference;
using Weapon = JunkyardDogs.Specifications.Weapon;

[Serializable]
public class WeaponBlueprint : PhysicalComponentBlueprint<WeaponBlueprintData>
{
}