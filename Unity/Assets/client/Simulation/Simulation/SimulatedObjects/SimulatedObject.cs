using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Simulation.Simulation
{
    public class SimulatedObject
    {
        public delegate void SimuatedObjectDelegate(SimulatedObject simulated);

        public event SimuatedObjectDelegate OnRemove;

        public SimulatedBody body = null;
        public SimulatedCollider collider = null;

        public virtual void OnCollide(SimulatedObject other)
        {

        }

        protected void Remove()
        {
            if (OnRemove != null)
                OnRemove(this);
        }

        public virtual void Update()
        {

        }
    }
}
