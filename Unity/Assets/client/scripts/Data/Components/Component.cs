using UnityEngine;
using System.Collections;
using JunkyardDogs.Specifications;
using System;
using Polenter.Serialization;
using WeakReference = Data.WeakReference;

namespace JunkyardDogs.Components
{
    [Serializable]
    public class Component<T> : GenericComponent where T:Specification
    {
        public WeakReference SpecificationReference { get; set; }

        [ExcludeFromSerialization]
        public Specification Specification
        {
            get
            {
                return SpecificationReference.Asset as Specification;
            }
        }

        [SerializeField]
        public Distinction[] _distinctions { get; set; }

        public override Specification GetSpecification()
        {
            return Specification;
        }

        public override void SetSpecification(WeakReference spec)
        {
            SpecificationReference.Reference = spec;
        }

        public Component()
        {
            SpecificationReference = new WeakReference();
        }
    }
}