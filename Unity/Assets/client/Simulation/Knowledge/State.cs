using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Simulation.Knowledge
{
    [CreateAssetMenu(fileName = "State", menuName = "Simulation/Knowledge/State", order = 2)]
    public class State : Knowledge
    {
        [SerializeField]
        private Information.State _state;

        public Information.State TargetState
        {
            get
            {
                return _state;
            }
        }

        public override bool IsTrue(Information.Information information)
        {
            return information.Opponent.State == _state;
        }
    }
}