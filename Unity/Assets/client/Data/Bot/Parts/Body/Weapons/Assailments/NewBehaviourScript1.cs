using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Bot
{
    public class MeleeWeapon : Assailer
    {
        private int _range;
        public int Range { get { return _range; } }
    }
}