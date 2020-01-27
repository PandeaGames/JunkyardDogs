namespace JunkyardDogs.Simulation
{
    public abstract class AbstractDecisionMove : IDecisionMaker
    {
        protected const int MAX_NUMBER_OF_MOVEMENT_TICKS = 40;
        
        public class DecisionMoveLogic : Logic
        {
            public int numberOfPreviousConcurrentMovementDecisions;
            public int maxNumberOfTicksForMovement;
            public bool shouldStopMoving;
        }

        public abstract Logic GetDecisionWeight(SimBot simBot, SimulatedEngagement engagement);
        public abstract void MakeDecision(SimBot simBot, SimulatedEngagement engagement);
    }
}
