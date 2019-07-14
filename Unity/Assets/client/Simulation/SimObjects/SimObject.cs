namespace JunkyardDogs.Simulation
{
    public class SimObject
    {
        protected SimulatedEngagement engagement;

        public SimObject(SimulatedEngagement engagement)
        {
            this.engagement = engagement;
        }

        public virtual void OnDrawGizmos()
        {
            
        }
    }
}