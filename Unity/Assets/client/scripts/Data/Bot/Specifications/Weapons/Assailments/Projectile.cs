using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Specifications
{
    [CreateAssetMenu(fileName = "Projectile", menuName = "Specifications/Projectile", order = 7)]
    public class Projectile : Assailer
    {
        private int _speed;

        public int Speed { get { return _speed; } }
    }
}