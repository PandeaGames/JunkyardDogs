using UnityEngine.Scripting;

namespace JunkyardDogs.Simulation
{
    [Preserve]
    public class DecisionStunned : IDecisionMaker
    {
        public class DecisionStunnedLogic : Logic
        {
            public bool isStunned;
        }
        
        public Logic GetDecisionWeight(SimBot simBot, SimulatedEngagement engagement)
        {
            DecisionStunnedLogic logic = new DecisionStunnedLogic();
            logic.isStunned = simBot.IsStunned();

            if (logic.isStunned)
            {
                logic.priority = DecisionPriority.Stunned;
            }
            else
            {
                logic.priority = DecisionPriority.None;
            }
            
            return logic;
        }

        public void MakeDecision(SimBot simBot, SimulatedEngagement engagement)
        {
            engagement.SendEvent(new StunDecisionEvent(simBot));
        }
    }
}