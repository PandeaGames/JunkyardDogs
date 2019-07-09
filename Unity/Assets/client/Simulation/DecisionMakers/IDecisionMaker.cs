namespace JunkyardDogs.Simulation
{
    public enum HardDecisions
    {
        Movement = 1000,
        PoweringWeapon = 1001,
        CooldownWeapon = 1002,
        Stunned = 1003
    }
    
    public interface IDecisionMaker
    {
        Logic GetDecisionWeight(SimBot simBot, SimulatedEngagement engagement);
        void MakeDecision(SimBot simBot, SimulatedEngagement engagement);
    }
}