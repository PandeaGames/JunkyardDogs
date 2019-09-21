using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Simulation.Simulation
{
    public class SimulatedBody
    {
        public Vector2 position;
        public Vector2 accelerationPerSecond;
        public Vector2 velocityPerSecond;
        public SimRotation rotation;
        public Vector2 scale = Vector2.one;
        public bool isTrigger = false;
        public bool isStatic;
        public float mass = 1;
        
        
        public Vector3 PositionToWorld()
        {
            return new Vector3(position.x, 0, position.y);
        }
    }
}
