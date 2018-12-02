using UnityEngine;
using JunkyardDogs.Specifications;
using System;
using JunkyardDogs.Components;
using Component = JunkyardDogs.Components.Component;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;
using Weapon = JunkyardDogs.Specifications.Weapon;

[Serializable]
public class WeaponBlueprint : PhysicalComponentBlueprint<WeaponBlueprintData>
{
}