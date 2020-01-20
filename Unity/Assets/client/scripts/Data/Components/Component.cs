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
    public interface IComponent : IConsumable
    {
        SpecificationStaticDataReference SpecificationReference { set; get; }
        ManufacturerStaticDataReference Manufacturer { set; get; }
        Specification Specification { get; }
    }
    
    [Serializable]
    public class Component<TSpecification> : IComponent, ComponentGrade.IGradedComponent where TSpecification:Specification
    {
        [SerializeField]
        private SpecificationStaticDataReference _specificationReference = new SpecificationStaticDataReference();
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

        public TSpecification GetSpec()
        {
            return (TSpecification) Specification;
        }
        
        public TGetSpecification GetSpec<TGetSpecification>() where TGetSpecification:TSpecification
        {
            return (TGetSpecification) Specification;
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