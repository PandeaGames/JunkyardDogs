using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JunkyardDogs.Components;
using JunkyardDogs.Simulation.Agent;

namespace JunkyardDogs
{
    [CreateAssetMenu(fileName = "Bot", menuName = "Bot", order = 1)]
    public class Bot : ScriptableObject
    {
        [SerializeField]
        private Motherboard _motherboard;

        [SerializeField]
        private Chassis _chassis;

        [SerializeField]
        private Agent _agent;

        public Chassis Chassis { get { return _chassis; } }
        public Motherboard Motherboard { get { return _motherboard; } }
        public Agent Agent { get { return _agent; } }
    }
}