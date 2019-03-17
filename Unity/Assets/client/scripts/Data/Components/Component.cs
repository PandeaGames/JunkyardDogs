using UnityEngine;
using System.Collections;
using JunkyardDogs.Specifications;
using System;
using Polenter.Serialization;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;
using System.Threading.Tasks;
using JunkyardDogs.Data;

namespace JunkyardDogs.Components
{
    [Serializable]
    public class Component
    {
        public SpecificationStaticDataReference SpecificationReference { get; set; }

        public ManufacturerStaticDataReference Manufacturer { get; set; }

        [ExcludeFromSerialization]
        public Specification Specification
        {
            get
            {
                return SpecificationReference.Data;
            }
        }

        [SerializeField]
        public Distinction[] _distinctions { get; set; }

        public Component()
        {
            SpecificationReference = new SpecificationStaticDataReference();
        }

        public virtual void Dismantle(Inventory inventory)
        {
            inventory.AddComponent(this);
        }
    }
}