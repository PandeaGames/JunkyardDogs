using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Specifications
{
    public class PhysicalSpecification : Specification
    {
        [SerializeField]
        private float _volume;
        public double Volume { get { return _volume; } }
    }
}