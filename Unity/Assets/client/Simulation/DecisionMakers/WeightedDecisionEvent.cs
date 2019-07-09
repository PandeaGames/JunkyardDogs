namespace JunkyardDogs.Simulation
{
    public class WeightedDecisionEvent : SimLogicEvent
    {
        public readonly SimBot.WeightedDecision decision;
        
        public WeightedDecisionEvent(SimBot.WeightedDecision decision)
        {
            this.decision = decision;
        }
    }
}