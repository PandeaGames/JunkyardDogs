using UnityEngine;
using System.Collections;
using JunkyardDogs.Specifications;
using System;

namespace JunkyardDogs.Components
{
    [Serializable]
    public class Component<T> : ScriptableObject where T:Specification
    {
        [SerializeField]
        private T _specification;

        [SerializeField]
        private Distinction[] _distinctions;

        public T Specification {
            get { return _specification; }
            set { _specification = value; }
        }
    }
}