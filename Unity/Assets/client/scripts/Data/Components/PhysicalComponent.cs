using UnityEngine;
using System.Collections;
using JunkyardDogs.Specifications;
using System;
using JunkyardDogs.Data;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

namespace JunkyardDogs.Components
{
    public class PhysicalComponent : Component
    {
        public MaterialStaticDataReference Material { get; set; }
    }
}