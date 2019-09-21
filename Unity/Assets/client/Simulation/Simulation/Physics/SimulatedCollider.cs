using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Simulation.Simulation
{
    public class SimulatedCollider
    {
        public Color gizmosColor = Color.grey;

        protected SimulatedBody _body;
        protected float _x;
        protected float _y;

        public Vector2 Position
        {
            get
            {
                return new Vector2(_x, _y);
            }
            set
            {
                _x = value.x;
                _y = value.y;
            }
        }

        public float x
        {
            get { return _x; }
            set { _x = value; }
        }
        
        public float y
        {
            get { return _y; }
            set { _y = value; }
        }

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