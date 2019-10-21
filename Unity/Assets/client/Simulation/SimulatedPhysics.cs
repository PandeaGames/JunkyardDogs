using System;
using System.Collections.Generic;
using JunkyardDogs.Simulation.Simulation;
using UnityEngine;

namespace JunkyardDogs.Simulation
{
    public class SimCollisionLoiterEvent : SimEvent
    {
        public SimPhysicsObject obj1;
        public SimPhysicsObject obj2;

        public SimCollisionLoiterEvent(SimPhysicsObject obj1, SimPhysicsObject obj2)
        {
            this.obj1 = obj1;
            this.obj2 = obj2;
        }
    }
    
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
        private float friction = 1.15f;
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
                        else
                        {
                            engagement.SendEvent(new SimCollisionLoiterEvent(simulatedPhysicsObj, otherSimulatedPhysicsObj));
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

            p2.x += Mathf.Cos(line.angle) * 20;
            p2.y += Mathf.Sin(line.angle) * 20;

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
                float t1 = ((m*-1) * b + Mathf.Sqrt(underRadical)) / (Mathf.Pow(m, 2) + 1); //one of the intercept x's
                float t2 = ((m *-1) * b - Mathf.Sqrt(underRadical)) / (Mathf.Pow(m, 2) + 1); //other intercept's x
                Vector2 i1 = new Vector2(t1 + c.x, m * t1 + b + c.y);
                Vector2 i2 = new Vector2(t2 + c.x, m * t2 + b + c.y);
                // var i1 = { x:t1 + c.x, y:m * t1 + b + c.y }; //intercept point 1
                //var i2 = { x:t2 + c.x, y:m * t2 + b + c.y }; //intercept point 2
                if ((0 < t1 && t1 < 1) || (0 < t2 && t2 < 1))
                {
                    return true;
                }
                return false;
            }

            /*Vector2 intersection = ClosestIntersection(circle.Body.position.x, circle.Body.position.y, circle.radius, p1, p2);
            return intersection != Vector2.zero;*/

            //bool doesCollide = TestSweptCircleLine(circle.Position, circle.radius, Vector2.zero, p1, p2, 0f);

           // bool doesCollide = SolveCircleLine(p1, p2, circle.radius, circle.Position);
           bool doesCollide = Simple(p1, p2, circle.Position, 1);
            return doesCollide;
        }
//https://math.stackexchange.com/questions/275529/check-if-line-intersects-with-circles-perimeter
        private bool Simple(Vector2 p1, Vector3 p2, Vector2 cPos, float r)
        {
            float ax = p1.x;
            float ay = p1.y;

            float bx = p2.x;
            float by = p2.y;

            float cx = cPos.x;
            float cy = cPos.y;
            
            // parameters: ax ay bx by cx cy r
            ax -= cx;
            ay -= cy;
            bx -= cx;
            by -= cy;
            float a = Mathf.Pow(ax,2f) + Mathf.Pow(ay, 2f) - Mathf.Pow(r, 2f);
            float b = 2f*(ax*(bx - ax) + ay*(by - ay));
            float c = Mathf.Pow((bx - ax), 2f) + Mathf.Pow((by - ay),2f);
            float disc = Mathf.Pow(b,2f) - 4f*a*c;
            if (disc <= 0)
            {
                return false;
            }
            float sqrtdisc = Mathf.Sqrt(disc);
            float t1 = (-b + sqrtdisc)/(2f*a);
            float t2 = (-b - sqrtdisc)/(2f*a);
            if ((0 < t1 && t1 < 1) || (0 < t2 && t2 < 1))
            {
                return true;
            }
            return false;
        }
        //cx,cy is center point of the circle 
public Vector2 ClosestIntersection(float cx, float cy, float radius,
                                  Vector2 lineStart, Vector2 lineEnd)
{
    Vector2 intersection1;
    Vector2 intersection2;
    int intersections = FindLineCircleIntersections(cx, cy, radius, lineStart, lineEnd, out intersection1, out intersection2);

    if (intersections == 1)
        return intersection1; // one intersection

    if (intersections == 2)
    {
        double dist1 = Distance(intersection1, lineStart);
        double dist2 = Distance(intersection2, lineStart);

        if (dist1 < dist2)
            return intersection1;
        else
            return intersection2;
    }

    return Vector2.zero; // no intersections at all
}

private double Distance(Vector2 p1, Vector2 p2)
{
    return Math.Sqrt(Math.Pow(p2.x - p1.x, 2) + Math.Pow(p2.y - p1.y, 2));
}

// Find the points of intersection.
private int FindLineCircleIntersections(float cx, float cy, float radius,
                                        Vector2 point1, Vector2 point2, out               
                                        Vector2 intersection1, out Vector2 intersection2)
{
    float dx, dy, A, B, C, det, t;

    dx = point2.x - point1.x;
    dy = point2.y - point1.y;

    A = dx * dx + dy * dy;
    B = 2 * (dx * (point1.x - cx) + dy * (point1.y - cy));
    C = (point1.x - cx) * (point1.x - cx) + (point1.y - cy) * (point1.y - cy) - radius * radius;

    det = B * B - 4 * A * C;
    if ((A <= 0.0000001) || (det < 0))
    {
        // No real solutions.
        intersection1 = new Vector2(float.NaN, float.NaN);
        intersection2 = new Vector2(float.NaN, float.NaN);
        return 0;
    }
    else if (det == 0)
    {
        // One solution.
        t = -B / (2 * A);
        intersection1 = new Vector2(point1.x + t * dx, point1.y + t * dy);
        intersection2 = new Vector2(float.NaN, float.NaN);
        return 1;
    }
    else
    {
        // Two solutions.
        t = (float)((-B + Math.Sqrt(det)) / (2 * A));
        intersection1 = new Vector2(point1.x + t * dx, point1.y + t * dy);
        t = (float)((-B - Math.Sqrt(det)) / (2 * A));
        intersection2 = new Vector2(point1.x + t * dx, point1.y + t * dy);
        return 2;
    }
}
        /*
         * private bool DoesCollide(SimulatedCircleCollider circle
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
         */
        
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
        
        private bool TestSweptCircleLine(Vector2 c, float r, Vector2 d, Vector2 p0, Vector2 p1, float outT) {
            Vector2 p0p1 = p1 - p0;
            // we have this annoying case first, where the circle is *already* intersecting the line segment so check for that first.  In this case, t would be 0.0f.  As for intersection points technically you have 2, but your "solver" might just want to use c
            Vector2 closestPointOnLineSegment = p0 + p0p1 *  Mathf.Clamp01(Vector2.Dot(c - p0, p0p1) / Vector2.Dot(p0p1, p0p1));
            if (Vector2.Distance(closestPointOnLineSegment, c) <= r * r)
            {
               outT = 0.0f;
               return true;
            }
            Vector2 p0p1Perp = p0p1.PerpendicularClockwise();
            
            // can avoid normalisation here, but lets keep this simpler for now
            Vector2 pn = p0p1Perp.normalized;
            float   pd = -Vector2.Dot(pn, p0);
            // system of equations is:
            // dot(pn, X) = -pd +- r (X is any point that is +r or -r distance away from the infinite line)
            // c + d * t = X (t is in the 0 to 1 range inclusive, X in this case is again the center of the circle when it intersects the infinite line (not necessarily the segment))
            // using substitution and solving for T
            float distanceLineToCenterOfCircle = Vector2.Dot(pn, c) + pd; // just line signed plane distance
            float signSideOfLine = distanceLineToCenterOfCircle < 0 ? -1f:1f;
            // if our sign is positive, then our system of equations is + R
            // if not our system of equations is - R
            float modR = signSideOfLine * r;
            outT = (((-pd + modR) - Vector2.Dot(pn, c)) / (Vector2.Dot(pn, d)));
            // now validate t and s
            if (outT >= 0.0f && outT <= 1.0f)
            {
                // alright, our swept circle is intersecting the infinite line
                // now validate that this intersection is in the line segment
                // range too
                Vector2 ptCenterOfCircleWhenIntersecting = c + d * outT;
                Vector2 ptOnLineWhenIntersecting = ptCenterOfCircleWhenIntersecting + pn * -modR;
                // find the scalar projection of ptOnLineWhenIntersecting.  If it is between 0 and 1 inclusive we have an intersection
                float s = Vector2.Dot(ptOnLineWhenIntersecting - p0, p0p1) / Vector2.Dot(p0p1, p0p1);
                if (s >= 0.0f && s <= 1.0f)
                {
                    return true;
                }
            }
            return false;
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
        public static bool SolveCircleLine(Vector2 p1, Vector2 p2, float R, Vector2 circlePos)
        {
            float Bx = p2.x;
            float By = p2.y;
            float Ax = p1.x;
            float Ay = p1.y;

            float Cx = circlePos.x;
            float Cy = circlePos.y;
            
            
            // compute the euclidean distance between A and B
           float  LAB = Mathf.Sqrt( (Bx-Ax) * (Bx-Ax)+(By-Ay)*(By-Ay));

// compute the direction vector D from A to B
            float Dx = (Bx - Ax) / LAB;
            float Dy = (By - Ay) / LAB;

// the equation of the line AB is x = Dx*t + Ax, y = Dy*t + Ay with 0 <= t <= LAB.

// compute the distance between the points A and E, where
// E is the point of AB closest the circle center (Cx, Cy)
            float t = Dx * (Cx - Ax) + Dy * (Cy - Ay);   

// compute the coordinates of the point E
            float Ex = t * Dx + Ax;
            float Ey = t * Dy + Ay;

// compute the euclidean distance between E and C
            float LEC = Mathf.Sqrt((Ex-Cx) * (Ex-Cx) + (Ey-Cy) * (Ey-Cy));

// test if the line intersects the circle
            if( LEC < R )
            {
               
                // compute distance from t to circle intersection point
                float dt = Mathf.Sqrt( R * R - LEC * LEC);
                float t1 = t - dt;
                float t2 = t + dt;
                // compute first intersection point
                float Fx = (t - dt) * Dx + Ax;
                float Fy = (t - dt) * Dy + Ay;

                // compute second intersection point
                float Gx = (t + dt) * Dx + Ax;
                float Gy = (t + dt) * Dy + Ay;

                return true;
            }

// else test if the line is tangent to circle
            else if (LEC == R)
            {
                return true;
            }
                // tangent point to circle is E

                else

            {
                
            }
            // line doesn't touch circle

            return false;
        }
    }
    
    
}