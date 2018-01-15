using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Specifications
{
    public class PhysicalSpecification : Specification
    {
        [SerializeField]
        private Material _material;

        [SerializeField]
        private float _volume;

        public Material Material { get { return _material; } }
        public double Volume { get { return _volume; } }
    }
}