using System;
using System.Collections.Generic;
using JunkyardDogs.Simulation.Simulation;
using UnityEngine;

namespace JunkyardDogs.Simulation
{
    public class SimCollisionEvent : SimEvent
    {
        public SimPhysicsObject obj1;
        public SimPhysicsObject obj2;

        public SimCollisionEvent(SimPhysicsObject obj1, SimPhysicsObject obj2)
        {
            this.obj1 = obj1;
            this.obj2 = obj2;
        }
    }
    
    public class SimCollisionExitEvent : SimEvent
    {
        public SimPhysicsObject obj1;
        public SimPhysicsObject obj2;

        public SimCollisionExitEvent(SimPhysicsObject obj1, SimPhysicsObject obj2)
        {
            this.obj1 = obj1;
            this.obj2 = obj2;
        }
    }
    
    public class SimulatedPhysics : ISimulatedEngagementGlobalEventHandler
    {
        private float friction = 1.25f;
        private Dictionary<SimObject, HashSet<SimObject>> _collisionTable = new Dictionary<SimObject, HashSet<SimObject>>();
        
        public static void Step(SimulatedEngagement engagement)
        {
            
        }
        
        public Type[] EventsToHandle()
        {
            return new Type[] {typeof(SimCollisionEvent), typeof(SimPostLogicEvent), typeof(SimInstantiationEvent), typeof(SimDestroyEvent)};
        }

        public void OnSimEvent(SimulatedEngagement engagement, SimEvent simEvent)
        {
            if (simEvent is SimCollisionEvent)
            {
                OnSimEvent(engagement, simEvent as SimCollisionEvent);
            }
            else if (simEvent is SimPostLogicEvent)
            {
                OnSimEvent(engagement, simEvent as SimPostLogicEvent);
            }
            else if(simEvent is SimInstantiationEvent)
            {
                OnSimEvent(engagement, simEvent as SimInstantiationEvent);
            }
            else if(simEvent is SimDestroyEvent)
            {
                OnSimEvent(engagement, simEvent as SimDestroyEvent);
            }
        }
        
        public void OnSimEvent(SimulatedEngagement engagement, SimCollisionEvent simEvent)
        {
            
        }
        
        public void OnSimEvent(SimulatedEngagement engagement, SimInstantiationEvent simEvent)
        {
            _collisionTable.Add(simEvent.instance, new HashSet<SimObject>());
        }
        
        public void OnSimEvent(SimulatedEngagement engagement, SimDestroyEvent simEvent)
        {
            HashSet<SimObject> hashSet = _collisionTable[simEvent.instance];

            foreach (SimObject other in hashSet)
            {
                engagement.SendEvent(new SimCollisionExitEvent(simEvent.instance as SimPhysicsObject, other as SimPhysicsObject));
            }
            
            hashSet.Clear();
            _collisionTable.Remove(simEvent.instance);
        }
        
        public void OnSimEvent(SimulatedEngagement engagement, SimPostLogicEvent simEvent)
        {
            //Update positions
            foreach(SimObject simulated in engagement.Objects)
            {
                SimPhysicsObject simulatedPhysicsObj = simulated as SimPhysicsObject;
                if (simulatedPhysicsObj != null)
                {
                    simulatedPhysicsObj.body.velocityPerSecond = new Vector2(
                        simulatedPhysicsObj.body.velocityPerSecond.x + simulatedPhysicsObj.body.accelerationPerSecond.x,
                        simulatedPhysicsObj.body.velocityPerSecond.y + simulatedPhysicsObj.body.accelerationPerSecond.y
                        );
                    
                    SimRotation velocityAngle = new SimRotation();
                    velocityAngle.Set(simulatedPhysicsObj.body.velocityPerSecond);

                    SimRotation directionalVelocityAngle = new SimRotation();
                    directionalVelocityAngle.deg360 = velocityAngle.deg360 -90;
                    
                    SimRotation moveAngle = directionalVelocityAngle + simulatedPhysicsObj.body.rotation;
                    
                    float angle = moveAngle.rad;

                    Vector2 vector = new Vector2(
                        Mathf.Cos(angle) * simulatedPhysicsObj.body.velocityPerSecond.magnitude,
                        Mathf.Sin(angle) * simulatedPhysicsObj.body.velocityPerSecond.magnitude);
                    
                    simulatedPhysicsObj.body.position = simulatedPhysicsObj.body.position + (vector * SimulatedEngagement.SimuationStep);
                    simulatedPhysicsObj.body.velocityPerSecond = new Vector2(
                        simulatedPhysicsObj.body.velocityPerSecond.x / friction,
                        simulatedPhysicsObj.body.velocityPerSecond.y / friction
                    );
                }
            }

            //Calculate collisions
            for (int i = 0; i<engagement.Objects.Count - 1;i++)
            {
                SimObject simulated = engagement.Objects[i];
                SimPhysicsObject simulatedPhysicsObj = simulated as SimPhysicsObject;
                if (!IsValidForCollision(simulatedPhysicsObj))
                    continue;

                for (int j = i + 1; j < engagement.Objects.Count; j++)
                {
                    SimObject other = engagement.Objects[j];
                    SimPhysicsObject otherSimulatedPhysicsObj = other as SimPhysicsObject;

                    if (!IsValidForCollision(otherSimulatedPhysicsObj))
                        continue;
                    
                    if(DoesCollide(simulatedPhysicsObj, otherSimulatedPhysicsObj))
                    {
                        ResolveCollision(simulatedPhysicsObj, otherSimulatedPhysicsObj);

                        if (!_collisionTable[simulated].Contains(other))
                        {
                            engagement.SendEvent(new SimCollisionEvent(simulatedPhysicsObj, otherSimulatedPhysicsObj));
                            _collisionTable[simulated].Add(other);
                            _collisionTable[other].Add(simulated);
                        }
                    }
                    else if(_collisionTable[simulated].Contains(other))
                    {
                        engagement.SendEvent(new SimCollisionExitEvent(simulatedPhysicsObj, otherSimulatedPhysicsObj));
                        _collisionTable[simulated].Remove(other);
                        _collisionTable[other].Remove(simulated);
                    }
                }
            }
        }

        private bool IsValidForCollision(SimPhysicsObject simObject)
        {
            if (simObject == null)
            {
                return false;
            }
            else if (simObject.body != null && simObject.colliders != null && simObject.colliders.Count > 0)
            {
                return true;
            }

            return false;
        }

        private bool DoesCollide(SimPhysicsObject primary, SimPhysicsObject other)
        {
            for (int i = 0; i < primary.colliders.Count; i++)
            {
                SimulatedCollider collider = primary.colliders[i];
                
                for (int j = 0; j < other.colliders.Count; j++)
                {
                    SimulatedCollider otherCollider = other.colliders[j];

                    if (DoesCollide(collider, otherCollider))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool DoesCollide(SimulatedCollider collider, SimulatedCollider other)
        {
            if (collider is SimulatedCircleCollider)
            {
                if (other is SimulatedCircleCollider)
                {
                    return DoesCollide(collider as SimulatedCircleCollider, other as SimulatedCircleCollider);
                }

                if (other is SimulatedLineCollider)
                {
                    return DoesCollide(collider as SimulatedCircleCollider, other as SimulatedLineCollider);
                }

                if (other is SimulatedArenaCollider)
                {
                    return DoesCollide(collider as SimulatedCircleCollider, other as SimulatedArenaCollider);
                }
            }
            else if (collider is SimulatedLineCollider)
            {
                if (other is SimulatedCircleCollider)
                {
                    return DoesCollide(other as SimulatedCircleCollider, collider as SimulatedLineCollider);
                }
                
                if (other is SimulatedLineCollider)
                {
                    return true;
                }
                
                if (other is SimulatedArenaCollider)
                {
                    return DoesCollide(collider as SimulatedLineCollider, other as SimulatedArenaCollider);
                }
            }
            else if (collider is SimulatedArenaCollider)
            {
                if (other is SimulatedCircleCollider)
                {
                    return DoesCollide(other as SimulatedCircleCollider, collider as SimulatedArenaCollider);
                }
                
                if (other is SimulatedLineCollider)
                {
                    return DoesCollide(other as SimulatedLineCollider, collider as SimulatedArenaCollider);
                }
                
                if (other is SimulatedArenaCollider)
                {
                    return false;
                }
            }

            return false;
        }

        private bool DoesCollide(SimulatedCircleCollider collider, SimulatedCircleCollider other)
        {
            float d = Vector2.Distance(collider.Body.position, other.Body.position);
            float r1 = collider.radius * collider.Body.scale.x;
            float r2 = other.radius * other.Body.scale.x;
            float r = r1 + r2;
            bool doesCollide = d < r;
            return doesCollide;
        }
        
        private bool DoesCollide(SimulatedCircleCollider circle
            , SimulatedLineCollider line)
        {
            Vector2 p1 = line.Body.position;
            Vector2 p2 = line.Body.position;

            p2.x += Mathf.Cos(line.angle) * 10;
            p2.y += Mathf.Sin(line.angle) * 10;

            Vector2 c = circle.Body.position;
            float r = circle.radius;

            Vector2 p3 = new Vector2(p1.x - c.x, p1.y - c.y);
            Vector2 p4 = new Vector2(p2.x - c.x, p2.y - c.y);
            //var p3 = { x:p1.x - c.x, y: p1.y - c.y}; //shifted line points
            //var p4 = { x:p2.x - c.x, y: p2.y - c.y};

            float m = (p4.y - p3.y) / (p4.x - p3.x); //slope of the line
            float b = p3.y - m * p3.x; //y-intercept of line

            float underRadical = Mathf.Pow(r, 2) * Mathf.Pow(m, 2) + Mathf.Pow(r, 2) - Mathf.Pow(b, 2); //the value under the square root sign 

            if (underRadical< 0) {
                //line completely missed
                return false;
            } else {
                float t1 = (-m * b + Mathf.Sqrt(underRadical)) / (Mathf.Pow(m, 2) + 1); //one of the intercept x's
                float t2 = (-m * b - Mathf.Sqrt(underRadical)) / (Mathf.Pow(m, 2) + 1); //other intercept's x
                Vector2 i1 = new Vector2(t1 + c.x, m * t1 + b + c.y);
                Vector2 i2 = new Vector2(t2 + c.x, m * t2 + b + c.y);
                // var i1 = { x:t1 + c.x, y:m * t1 + b + c.y }; //intercept point 1
                //var i2 = { x:t2 + c.x, y:m * t2 + b + c.y }; //intercept point 2
                return true;
            }
        }
        
        private bool DoesCollide(SimulatedLineCollider collider, SimulatedCircleCollider other)
        {
            return DoesCollide(other, collider);
        }
        
        private bool DoesCollide(SimulatedCircleCollider collider, SimulatedArenaCollider other)
        {
            if (collider.Body.position.x - collider.radius < other.Arena.Width / -2)
            {
                return true;
            }
            
            if (collider.Body.position.x + collider.radius > other.Arena.Width / 2)
            {
                return true;
            }
            
            if (collider.Body.position.y - collider.radius < other.Arena.Height / -2)
            {
                return true;
            }
            
            if (collider.Body.position.y + collider.radius > other.Arena.Height / 2)
            {
                return true;
            }
            
            return false;
        }
        
        private bool DoesCollide(SimulatedArenaCollider collider, SimulatedCircleCollider other)
        {
            return DoesCollide(other, collider);
        }
        
        private bool DoesCollide(SimulatedLineCollider collider, SimulatedArenaCollider other)
        {
            return true;
        }
        
        private bool DoesCollide(SimulatedArenaCollider collider, SimulatedLineCollider other)
        {
            return DoesCollide(other, collider);
        }

        private void ResolveCollision(SimPhysicsObject primary, SimPhysicsObject other)
        {
            if (primary.body.isStatic && other.body.isStatic
                || primary.body.isTrigger || other.body.isTrigger)
            {
                return;
            }
            
            for (int i = 0; i < primary.colliders.Count; i++)
            {
                SimulatedCollider collider = primary.colliders[i];
                
                for (int j = 0; j < other.colliders.Count; j++)
                {
                    SimulatedCollider otherCollider = other.colliders[j];
                    ResolveCollision(collider, otherCollider);
                }
            }
        }
        
        private void ResolveCollision(SimulatedCollider collider, SimulatedCollider other)
        {
            if (collider is SimulatedCircleCollider)
            {
                if (other is SimulatedCircleCollider)
                {
                    ResolveCollision(collider as SimulatedCircleCollider, other as SimulatedCircleCollider);
                }

                if (other is SimulatedArenaCollider)
                {
                    ResolveCollision(collider as SimulatedCircleCollider, other as SimulatedArenaCollider);
                }
            }
            else if (collider is SimulatedArenaCollider)
            {
                if (other is SimulatedCircleCollider)
                {
                    ResolveCollision(collider as SimulatedArenaCollider, other as SimulatedCircleCollider);
                }
            }
        }

        private void ResolveCollision(SimulatedCircleCollider collider, SimulatedCircleCollider other)
        {
            //resolving position
            var dx 			= other.Body.position.x - collider.Body.position.x;
            var dy 			= other.Body.position.y - collider.Body.position.y;
		
            var a 			= Mathf.Atan2(dy, dx);
            var d 			= Mathf.Sqrt(dx*dx+dy*dy);
            var cos 		= Mathf.Cos(a);
            var sin 		= Mathf.Sin(a);
		
            var vd 			= (collider.radius+other.radius) - d;
		
            var totalMass 	= collider.Body.mass+other.Body.mass;
		
            var c1vd 		= vd*(other.Body.mass/totalMass);
            var c2vd 		= vd - c1vd;
		
            collider.Body.position = new Vector2(collider.Body.position.x-cos*c1vd, collider.Body.position.y-sin*c1vd); 
            other.Body.position = new Vector2(other.Body.position.x+cos*c2vd, other.Body.position.y+sin*c2vd);
            
            /*float totalRadius = collider.radius + other.radius;
            float distance = Vector2.Distance(collider.Body.position, other.Body.position);
            float delta = 1 + (1-(distance / totalRadius));
            float x = collider.Body.position.x - other.Body.position.x;
            float dx =  x - totalRadius;
            float y = collider.Body.position.y - other.Body.position.y;
            float dy =  y - totalRadius;
            collider.Body.position = new Vector2(collider.Body.position.x - dx / 2, collider.Body.position.y - dy / 2);
            other.Body.position = new Vector2(other.Body.position.x + dx / 2, other.Body.position.y + dy / 2);
*/
        }
        
        private void ResolveCollision(SimulatedArenaCollider collider, SimulatedCircleCollider other)
        {
            ResolveCollision(other, collider);
        }

        private struct Dimensions
        {
            public float left;
            public float right;
            public float top;
            public float bottom;

            public Dimensions(float left,  float right, float top, float bottom)
            {
                this.left = left;
                this.right = right;
                this.top = top;
                this.bottom = bottom;
            }
            
        }
        
        private void ResolveCollision(SimulatedCircleCollider collider, SimulatedArenaCollider other)
        {
            Dimensions arena = new Dimensions(
                other.Arena.Width / -2,
                other.Arena.Width / 2,
                other.Arena.Height / -2,
                other.Arena.Height / 2
                );
            
            Dimensions circle = new Dimensions(
                collider.Body.position.x - collider.radius,
                collider.Body.position.x + collider.radius,
                collider.Body.position.y - collider.radius,
                collider.Body.position.y + collider.radius
            );
            
            if (circle.left < arena.left)
            {
                collider.Body.velocityPerSecond = new Vector2(
                    0,
                    collider.Body.velocityPerSecond.y
                );
                
                float delta = arena.left - circle.left;
                collider.Body.position = new Vector2(
                    collider.Body.position.x + delta,
                    collider.Body.position.y
                    );
            }
            
            if (circle.right > arena.right)
            {
                collider.Body.velocityPerSecond = new Vector2(
                    0,
                    collider.Body.velocityPerSecond.y
                );
                
                float delta = circle.right - arena.right;
                collider.Body.position = new Vector2(
                    collider.Body.position.x - delta,
                    collider.Body.position.y
                );
            }
            
            if (circle.top < arena.top)
            {
                collider.Body.velocityPerSecond = new Vector2(
                    collider.Body.velocityPerSecond.x,
                    0
                );
                
                float delta = arena.top - circle.top;
                collider.Body.position = new Vector2(
                    collider.Body.position.x,
                    collider.Body.position.y + delta
                );
            }
            
            if (circle.bottom > arena.bottom)
            {
                collider.Body.velocityPerSecond = new Vector2(
                    collider.Body.velocityPerSecond.x,
                    0
                    );
                
                float delta = circle.bottom - arena.bottom;
                collider.Body.position = new Vector2(
                    collider.Body.position.x,
                    collider.Body.position.y - delta
                );
            }
        }
    }
}