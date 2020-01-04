namespace JunkyardDogs.Simulation
{
    public abstract class AbstractDecisionMove : IDecisionMaker
    {
        public class DecisionMoveLogic : Logic
        {
            
        }

        public abstract Logic GetDecisionWeight(SimBot simBot, SimulatedEngagement engagement);
        public abstract void MakeDecision(SimBot simBot, SimulatedEngagement engagement);
    }
}
