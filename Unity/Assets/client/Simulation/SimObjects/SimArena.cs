namespace JunkyardDogs.Simulation
{
    public class SimArena : SimPhysicsObject
    {
        public SimArena(SimulatedEngagement simulatedEngagement, Arena arena) : base(simulatedEngagement)
        {
            collider = new SimulatedArenaCollider(body, arena);
        }
    }
}