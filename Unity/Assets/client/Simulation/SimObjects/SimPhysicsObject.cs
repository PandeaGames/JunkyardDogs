using JunkyardDogs.Simulation.Simulation;
using UnityEngine;

namespace JunkyardDogs.Simulation
{
    public class SimPhysicsObject : SimObject
    {
        public SimulatedCollider collider;
        public SimulatedBody body;
        
        public SimPhysicsObject(SimulatedEngagement engagement) : base(engagement)
        {
            body = new SimulatedBody();
        }

        public virtual void OnCollision(SimPhysicsObject other)
        {
            
        }
        
        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();

            if (collider != null)
            {
                collider.OnDrawGizmos();
            }
        }
    }
}