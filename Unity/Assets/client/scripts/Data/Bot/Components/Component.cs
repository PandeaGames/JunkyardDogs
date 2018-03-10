using UnityEngine;
using System.Collections;
using JunkyardDogs.Specifications;
using System;

namespace JunkyardDogs.Components
{
    [Serializable]
    public class Component<T> : GenericComponent where T:Specification
    {
        [SerializeField]
        private T _specification;

        [SerializeField]
        private Distinction[] _distinctions;

        public T Specification {
            get { return _specification; }
            set
            {
                _specification = value;
            }
        }

        public override Specification GetSpecification()
        {
            return _specification;
        }
    }
}