using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Specifications
{
    public class Assailer : PhysicalSpecification
    {
        [SerializeField]
        private float _damage;

        [SerializeField]
        private Effect[] _effects;

        public float Damage { get { return _damage; }
            set { _damage = value; }
        }
        public Effect[] Effects { get { return _effects; } }
    }
}
