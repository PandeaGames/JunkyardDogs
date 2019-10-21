namespace JunkyardDogs.Simulation
{
    public class WeightedDecisionEvent : SimLogicEvent
    {
        public readonly SimBotDecisionPlane.WeightedDecision decision;
        
        public WeightedDecisionEvent(SimBotDecisionPlane.WeightedDecision decision)
        {
            this.decision = decision;
        }
    }
}