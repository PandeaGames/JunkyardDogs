using System;

namespace JunkyardDogs.Components
{
    [Serializable]
    public class Engine : PhysicalComponent
    {
        public float StrafeAcceleration
        {
            get { return GetSpec<Specifications.Engine>().strafeAcceleration; }
        }
        
        public float StrafeMaxSpeed
        {
            get { return GetSpec<Specifications.Engine>().strafeMaxSpeed; }
        }
        
        public float ForwardAcceleration
        {
            get { return GetSpec<Specifications.Engine>().forwardAcceleration; }
        }
        
        public float ForwardMaxSpeed
        {
            get { return GetSpec<Specifications.Engine>().forwardAcceleration; }
        }
        
        public float BackwardAcceleration
        {
            get { return GetSpec<Specifications.Engine>().backwardAcceleration; }
        }
        
        public float BackwardMaxSpeed
        {
            get { return GetSpec<Specifications.Engine>().backwardMaxSpeed; }
        }
        
        public Engine()
        {

        }
    }
}