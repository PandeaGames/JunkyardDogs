using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Specifications
{
    [CreateAssetMenu(fileName = "Projectile", menuName = "Specifications/Projectile", order = 7)]
    public class Projectile : Assailer
    {
        [SerializeField]
        private float _speed;
        [SerializeField]
        private float _radius;

        public float Speed { get { return _speed; } }
        public float Radius { get { return _radius; } }
    }
}