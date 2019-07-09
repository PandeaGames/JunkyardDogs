using JunkyardDogs.Simulation.Simulation;
using UnityEngine;

namespace JunkyardDogs.Simulation
{
    public class SimPhysicsObject : SimObject
    {
        public Vector2 position;
        public Vector2 velocityPerSecond;
        public float radius;
        public SimulatedCollider collider;
        
        public SimPhysicsObject(SimulatedEngagement engagement) : base(engagement)
        {
            
        }
    }
}