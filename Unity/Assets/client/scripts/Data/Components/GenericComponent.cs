using UnityEngine;
using System.Collections;
using System;
using JunkyardDogs.Specifications;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

namespace JunkyardDogs.Components
{
    [Serializable]
    public abstract class GenericComponent
    {
        public Manufacturer Manufacturer { get; set; }

        public abstract Specification GetSpecification();
        public abstract void SetSpecification(WeakReference spec);

        public GenericComponent()
        {

        }
    }
}
