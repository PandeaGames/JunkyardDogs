namespace JunkyardDogs.Simulation
{
    public class SimArena : SimPhysicsObject
    {
        public SimArena(SimulatedEngagement simulatedEngagement, Arena arena) : base(simulatedEngagement)
        {
            colliders.Add(new SimulatedArenaCollider(body, arena));
        }
    }
}