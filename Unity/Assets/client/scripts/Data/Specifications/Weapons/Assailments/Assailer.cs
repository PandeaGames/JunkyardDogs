using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Specifications
{
    public class Assailer : PhysicalSpecification
    {
        [SerializeField]
        private int _damage;

        [SerializeField]
        private Effect[] _effects;

        public int Damage { get { return _damage; }
            set { _damage = value; }
        }
        public Effect[] Effects { get { return _effects; } }
    }
}
