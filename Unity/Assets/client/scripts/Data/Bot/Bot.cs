using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JunkyardDogs.Components;

namespace JunkyardDogs
{
    [CreateAssetMenu(fileName = "Bot", menuName = "Bot", order = 1)]
    public class Bot : ScriptableObject
    {
        [SerializeField]
        private Motherboard _motherboard;

        [SerializeField]
        private Chassis _chassis;

        public Chassis Chassis { get { return _chassis; } }
        public Motherboard Motherboard { get { return _motherboard; } }
    }
}