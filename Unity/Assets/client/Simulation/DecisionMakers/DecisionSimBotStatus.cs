
using JunkyardDogs.Simulation;

public class DecisionSimBotStatus : IDecisionMaker
{
    public class DecisionSimBotStatusLogic : Logic
    {
        public double remainingHealth;
        public double remainingOponentHealth;
        
    }
    public Logic GetDecisionWeight(SimBot simBot, SimulatedEngagement engagement)
    {
        DecisionSimBotStatusLogic logic = new DecisionSimBotStatusLogic();
        logic.priority = DecisionPriority.None;
        logic.remainingHealth = simBot.RemainingHealth;
        logic.remainingOponentHealth = simBot.opponent.RemainingHealth;

        return logic;
    }

    public void MakeDecision(SimBot simBot, SimulatedEngagement engagement)
    {
        
    }
}
