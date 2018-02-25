using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Simulation.Simulation
{
    public class SimulatedCircleCollider : SimulatedCollider
    {
        public float radius;

        public SimulatedCircleCollider(SimulatedBody body) : base(body)
        {

        }

        public override void OnDrawGizmos()
        {
            Gizmos.color = gizmosColor;



            Gizmos.DrawSphere(new Vector3(_body.position.x, 0 ,_body.position.y), radius);
        }
    }
}