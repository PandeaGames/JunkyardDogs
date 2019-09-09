using UnityEngine;

namespace JunkyardDogs.Simulation
{
    public class Arena
    {
        public Vector2 dimensions;

        public float Width
        {
            get { return dimensions.x; }
        }

        public float Height
        {
            get { return dimensions.y; }
        }
    }
}