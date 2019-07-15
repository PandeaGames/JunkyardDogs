using System;
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
    
    public class SimulatedPhysics : ISimulatedEngagementGlobalEventHandler
    {
        public static void Step(SimulatedEngagement engagement)
        {
            
        }
        
        public Type[] EventsToHandle()
        {
            return new Type[] {typeof(SimCollisionEvent), typeof(SimPostLogicEvent)};
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
        }
        
        public void OnSimEvent(SimulatedEngagement engagement, SimCollisionEvent simEvent)
        {
            
        }
        
        public void OnSimEvent(SimulatedEngagement engagement, SimPostLogicEvent simEvent)
        {
            //Update positions
            foreach(SimObject simulated in engagement.Objects)
            {
                SimPhysicsObject simulatedPhysicsObj = simulated as SimPhysicsObject;
                if (simulatedPhysicsObj != null)
                {
                    float angle = simulatedPhysicsObj.body.rotation.eulerAngles.y;

                    Vector2 vector = new Vector2(
                        Mathf.Cos(angle) * simulatedPhysicsObj.body.velocityPerSecond.x,
                        Mathf.Sin(angle) * simulatedPhysicsObj.body.velocityPerSecond.y);

                    simulatedPhysicsObj.body.position = simulatedPhysicsObj.body.position + (vector * SimulatedEngagement.SimuationStep);
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
                         
                    if(DoesCollide(simulatedPhysicsObj.collider, otherSimulatedPhysicsObj.collider))
                    {
                        engagement.SendEvent(new SimCollisionEvent(simulatedPhysicsObj, otherSimulatedPhysicsObj));
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
            else if (simObject.body != null && simObject.collider != null)
            {
                return simObject.body.doesCollide;
            }

            return false;
        }
        
        private bool DoesCollide(SimulatedCollider collider, SimulatedCollider other)
        {
            SimulatedCircleCollider colliderCircle = collider as SimulatedCircleCollider;
            SimulatedCircleCollider otherCircle = other as SimulatedCircleCollider;

            SimulatedLineCollider colliderLine = collider as SimulatedLineCollider;
            SimulatedLineCollider otherLine = other as SimulatedLineCollider;

            if (colliderCircle != null && otherCircle != null)
            {
                if (Vector2.Distance(colliderCircle.Body.position, otherCircle.Body.position) < colliderCircle.radius + otherCircle.radius)
                {
                    return true;
                }
            }

            if(colliderLine != null && otherLine != null)
            {
                return false;
            }

            if (colliderLine != null || otherLine != null)
            {
                SimulatedLineCollider line = colliderLine == null ? otherLine : colliderLine;
                SimulatedCircleCollider circle = colliderLine == null ? colliderCircle : otherCircle;

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

            return false;
        }
    }
}