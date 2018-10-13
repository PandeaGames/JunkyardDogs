using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Simulation.Simulation
{
    public class SimulatedCollider
    {
        public Color gizmosColor = Color.grey;

        protected SimulatedBody _body;

        public SimulatedBody Body { get { return _body; } }

        public SimulatedCollider(SimulatedBody body)
        {
            _body = body;
        }

        public virtual void OnDrawGizmos()
        {

        }

        public Vector3 PositionToWorld()
        {
            return new Vector3(_body.position.x, 0, _body.position.y);
        }
    }
}