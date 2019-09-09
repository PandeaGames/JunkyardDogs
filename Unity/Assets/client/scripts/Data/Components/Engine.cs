using System;

namespace JunkyardDogs.Components
{
    [Serializable]
    public class Engine : PhysicalComponent
    {
        public float Acceleration
        {
            get { return GetSpec<Specifications.Engine>().acceleration; }
        }
        
        public float MaxSpeed
        {
            get { return GetSpec<Specifications.Engine>().maxSpeed; }
        }
        
        public Engine()
        {

        }
    }
}