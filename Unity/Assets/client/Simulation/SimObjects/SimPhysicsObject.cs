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

            float angle = body.rotation.rad;

            float lengthOfRotationLine = 2;
            
            Vector3 vector = new Vector3(
                Mathf.Cos(angle) * lengthOfRotationLine,
                0,
                Mathf.Sin(angle) * lengthOfRotationLine);
            Color oldColor = Gizmos.color;
            Gizmos.color = Color.green;
            Gizmos.DrawLine(
                new Vector3(body.position.x, 0, body.position.y),
                new Vector3(body.position.x, 0, body.position.y) + vector
                );
            Gizmos.color = oldColor;
            
            if (collider != null)
            {
                collider.OnDrawGizmos();
            }
        }
    }
}