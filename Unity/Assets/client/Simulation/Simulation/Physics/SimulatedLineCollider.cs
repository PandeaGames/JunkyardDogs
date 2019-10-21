using System;
using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Simulation.Simulation
{
    public class SimulatedLineCollider : SimulatedCollider
    {
        public float angle;

        public SimulatedLineCollider(SimulatedBody body) : base(body)
        {

        }

        public override void OnDrawGizmos()
        {
            Vector3 from = new Vector3(_body.position.x, 0, _body.position.y);
            Vector3 to = new Vector3(from.x + Mathf.Cos(angle) * 10f,
                0, 
                from.z + Mathf.Sin(angle) * 10f);
            
            //Mathf.Cos(line.angle) * 10
                
            /*Vector3 to = new Vector3(from.x + Mathf.Cos(angle) * 10,
                0, 
                from.y + Mathf.Sin(angle) * 10);*/

            Color gizmosColor = Gizmos.color;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(from, to);
            Gizmos.color = gizmosColor;
        }
    }
}