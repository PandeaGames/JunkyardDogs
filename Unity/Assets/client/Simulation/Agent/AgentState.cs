using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JunkyardDogs.Simulation.Knowledge;

namespace JunkyardDogs.Simulation.Agent
{
    [CreateAssetMenu(fileName = "AgentState", menuName = "Simulation/Agent/AgentState", order = 2)]
    public class AgentState : ScriptableObject
    {
        [SerializeField]
        private Knowledge.State _state;

        [SerializeField]
        private List<Directive> _directives;

        [SerializeField]
        private List<StateTransition> _transitions;

        public Knowledge.State State
        {
            get { return _state; }
            set { _state = value; }
        }

        public List<Directive> Directives
        {
            get { return _directives; }
        }

        public List<StateTransition> Transitions
        {
            get { return _transitions; }
        }
    }
}