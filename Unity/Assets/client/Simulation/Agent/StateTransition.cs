using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using JunkyardDogs.Simulation.Knowledge;

namespace JunkyardDogs.Simulation.Agent
{
    [CreateAssetMenu(fileName = "State", menuName = "Simulation/Agent/StateTransition", order = 2)]
    public class StateTransition : ScriptableObject
    {
        [SerializeField]
        private List<Knowledge.Knowledge> _criteria;
           
        [SerializeField]
        private AgentState _stateToTransition;

        public List<Knowledge.Knowledge> Criteria
        {
            get
            {
                return _criteria;
            }
        }

        public AgentState StateToTransition
        {
            get
            {
                return _stateToTransition;
            }
        }
    }
}