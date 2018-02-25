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
            Vector3 line = _body.position;

            line.x += Mathf.Cos(angle) * 10;
            line.z += Mathf.Sin(angle) * 10;

            Gizmos.color = gizmosColor;
            Gizmos.DrawLine(_body.position, line);
        }
    }
}