namespace JunkyardDogs.Simulation
{
    public enum DecisionPriority
    {
        None = -1,
        Normal = 1,
        Movement = 2,
        PoweringWeapon = 3,
        CooldownWeapon = 4,
        Stunned = 5,
        FireWeapon = 6
    }
    
    public enum DecisionPlane
    {
        Movement = 1,
        Base = 0
    }
    
    public interface IDecisionMaker
    {
        Logic GetDecisionWeight(SimBot simBot, SimulatedEngagement engagement);
        void MakeDecision(SimBot simBot, SimulatedEngagement engagement);
    }
}