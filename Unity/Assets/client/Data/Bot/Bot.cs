using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JunkyardDogs.Bot
{
    public class Bot : Specification
    {
        [SerializeField]
        private Motherboard _motherboard;

        [SerializeField]
        private Chassis _chassis;

        public Chassis Chassis { get { return _chassis; } }
        public Motherboard Motherboard { get { return _motherboard; } }
    }
}