using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Simulation
{
    [CreateAssetMenu(fileName = "Engagement", menuName = "Simulation/Engagement", order = 2)]
    public class Engagement : ScriptableObject
    {
        [SerializeField]
        private Bot _redCombatent;

        [SerializeField]
        private Bot _blueCombatent;

        [SerializeField]
        private RulesOfEngagement _rules;

        [SerializeField]
        private double _seed;

        public Bot RedCombatent
        {
            get { return _redCombatent; }
            set { _redCombatent = value; }
        }

        public Bot BlueCombatent
        {
            get { return _blueCombatent; }
            set { _blueCombatent = value; }
        }

        public RulesOfEngagement Rules
        {
            get { return _rules; }
        }

        public double Seed
        {
            get
            {
                return _seed;
            }
        }
    }

    public enum Initiator
    {
        BLUE, 
        RED
    };

    [System.Serializable]
    public struct RulesOfEngagement
    {
        public Initiator initiator;
    }
}
