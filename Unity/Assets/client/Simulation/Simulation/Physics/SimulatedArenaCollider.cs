using JunkyardDogs.Simulation.Simulation;

namespace JunkyardDogs.Simulation
{
    public class SimulatedArenaCollider : SimulatedCollider
    {
        public Arena Arena { private set; get; }
        public SimulatedArenaCollider(SimulatedBody body, Arena arena) : base(body)
        {
            Arena = arena;
        }
    }
}