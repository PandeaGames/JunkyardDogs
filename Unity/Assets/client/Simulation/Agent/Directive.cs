using UnityEngine;
using System.Collections;
using JunkyardDogs.Simulation.Behavior;

namespace JunkyardDogs.Simulation.Agent
{
    [CreateAssetMenu(fileName = "Directive", menuName = "Simulation/Agent/Directive", order = 2)]
    public class Directive : ScriptableObject
    {
        [SerializeField]
        private Behavior.Action _action;

        public Behavior.Action Action
        {
            get { return _action; }
            set { _action = value; }
        }
    }
}