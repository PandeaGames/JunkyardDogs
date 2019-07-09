namespace JunkyardDogs.Simulation
{
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
                logic.weight = (int) HardDecisions.Stunned;
            }
            else
            {
                logic.weight = -1;
            }
            
            return logic;
        }

        public void MakeDecision(SimBot simBot, SimulatedEngagement engagement)
        {
            engagement.SendEvent(new StunDecisionEvent(simBot));
        }
    }
}