using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace JunkyardDogs.Simulation.Agent
{
    [CreateAssetMenu(fileName = "Agent", menuName = "Simulation/Agent/Agent", order = 2)]
    public class Agent : ScriptableObject
    {
        [SerializeField]
        private List<AgentState> _states;

        [SerializeField]
        private AgentState _initialState;

        public List<AgentState> States
        {
            get { return _states; }
        }

        public AgentState InitialState
        {
            get { return _initialState; }
        }
    }
}