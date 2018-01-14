using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Bot
{
    public class Assailer : ScriptableObject
    {
        [SerializeField]
        private int _damage;

        [SerializeField]
        private Effect[] _effects;

        public int Damage { get { return _damage; } }
    }
}
