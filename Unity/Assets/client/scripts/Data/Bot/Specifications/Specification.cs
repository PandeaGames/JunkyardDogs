using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Specifications
{
    public class Specification : ScriptableObject
    {
        [SerializeField]
        private Manufacturer _manufacturer;

        public Manufacturer Manufacturer { get { return _manufacturer; } }
    }
}