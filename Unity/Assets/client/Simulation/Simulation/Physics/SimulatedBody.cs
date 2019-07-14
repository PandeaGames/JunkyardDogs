using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Simulation.Simulation
{
    public class SimulatedBody
    {
        public Vector2 position;
        public Vector2 velocityPerSecond;
        public Quaternion rotation;
        public bool doesCollide = true;
        
        public Vector3 PositionToWorld()
        {
            return new Vector3(position.x, 0, position.y);
        }
    }
}
