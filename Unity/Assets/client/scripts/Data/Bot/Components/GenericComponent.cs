using UnityEngine;
using System.Collections;
using JunkyardDogs.Specifications;

namespace JunkyardDogs.Components
{
    public abstract class GenericComponent : ScriptableObject
    {
        [SerializeField]
        protected Manufacturer _manufacturer;

        public Manufacturer Manufacturer {
            get { return _manufacturer; }
            set { _manufacturer = value; }
        }

        public abstract Specification GetSpecification();
    }
}
