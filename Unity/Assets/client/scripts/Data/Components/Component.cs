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
    public class Component : ComponentGrade.IGradedComponent
    {
        [SerializeField]
        private SpecificationStaticDataReference _specificationReference;
        [SerializeField]
        private ManufacturerStaticDataReference _manufacturer;
        
        public SpecificationStaticDataReference SpecificationReference
        {
            get { return _specificationReference;}
            set { _specificationReference = value; }
        }

        public ManufacturerStaticDataReference Manufacturer
        {
            get { return _manufacturer;}
            set { _manufacturer = value; }
        }

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

        public TSpec GetSpec<TSpec>() where TSpec : Specification
        {
            return Specification as TSpec;
        }
        
        public bool IsSpec<TSpec>() where TSpec : Specification
        {
            return Specification is TSpec;
        }

        public virtual void Dismantle(Inventory inventory)
        {
            inventory.AddComponent(this);
        }

        public virtual ComponentGrade Grade
        {
            get { return _specificationReference.Data.Grade; }
        }
        
        public Rarity Rarity
        {
            get { return _specificationReference.Data.Rarity + _manufacturer.Data.RarityBonus; }
        }
    }
}