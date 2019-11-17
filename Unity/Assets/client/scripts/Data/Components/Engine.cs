using System;

namespace JunkyardDogs.Components
{
    [Serializable]
    public class Engine : PhysicalComponent<Specifications.Engine>
    {
        public float StrafeAcceleration
        {
            get { return GetSpec().strafeAcceleration; }
        }
        
        public float StrafeMaxSpeed
        {
            get { return GetSpec().strafeMaxSpeed; }
        }
        
        public float ForwardAcceleration
        {
            get { return GetSpec().forwardAcceleration; }
        }
        
        public float ForwardMaxSpeed
        {
            get { return GetSpec().forwardAcceleration; }
        }
        
        public float BackwardAcceleration
        {
            get { return GetSpec().backwardAcceleration; }
        }
        
        public float BackwardMaxSpeed
        {
            get { return GetSpec().backwardMaxSpeed; }
        }
        
        public Engine()
        {

        }
    }
}