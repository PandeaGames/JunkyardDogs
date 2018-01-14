using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Bot
{
    public class Projectile : Assailer
    {
        private int _speed;

        public int Speed { get { return _speed; } }
    }
}