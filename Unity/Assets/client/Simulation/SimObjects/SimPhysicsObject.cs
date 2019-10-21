using System.Collections.Generic;
using JunkyardDogs.Simulation.Simulation;
using UnityEngine;

namespace JunkyardDogs.Simulation
{
    public class SimPhysicsObject : SimObject
    {
        public List<SimulatedCollider> colliders;
        public SimulatedBody body;
        
        public SimPhysicsObject(SimulatedEngagement engagement) : base(engagement)
        {
            body = new SimulatedBody();
            colliders = new List<SimulatedCollider>();
        }

        public virtual void OnCollision(SimPhysicsObject other)
        {
            
        }
        
        public virtual void OnCollisionLoiter(SimPhysicsObject other)
        {
            
        }
        
        public virtual void OnCollisionExit(SimPhysicsObject other)
        {
            
        }

        public Rect GetBounds()
        {
            Rect bounds = new Rect();

            foreach (SimulatedCollider collider in colliders)
            {
                SimulatedCircleCollider circleCollider = collider as SimulatedCircleCollider;

                if (circleCollider != null)
                {
                    if (bounds.x > circleCollider.x - circleCollider.radius)
                    {
                        bounds.x = circleCollider.x - circleCollider.radius;
                    }
                    
                    if (bounds.x + bounds.width < circleCollider.x + circleCollider.radius)
                    {
                        float adjustment = bounds.x < 0 ? bounds.x * -1 : 0;
                        float adjustedX = bounds.x + adjustment;
                        bounds.width = ((circleCollider.x + adjustment) + circleCollider.radius)-adjustedX;
                    }
                    
                    if (bounds.y > circleCollider.y - circleCollider.radius)
                    {
                        bounds.y = circleCollider.y - circleCollider.radius;
                    }
                    
                    if (bounds.y + bounds.height < circleCollider.y + circleCollider.radius)
                    {
                        float adjustment = bounds.y < 0 ? bounds.y * -1 : 0;
                        float adjustedX = bounds.y + adjustment;
                        bounds.height = adjustedX -(circleCollider.y + circleCollider.radius);
                    }
                }
            }

            return bounds;
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
            
            if (colliders != null)
            {
                foreach (SimulatedCollider collider in colliders)
                {
                    collider.OnDrawGizmos();
                }
            }
        }
    }
}