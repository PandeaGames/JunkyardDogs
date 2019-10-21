namespace JunkyardDogs.Simulation
{
    public class SimObject
    {
        protected SimulatedEngagement engagement;
        protected double instantiationTime;

        public void SetInstanceID(int instanceID)
        {
            this.instanceId = instanceID;
        }
        private int instanceId;

        public override int GetHashCode()
        {
            return instanceId;
        }

        public SimObject(SimulatedEngagement engagement)
        {
            this.engagement = engagement;
            instantiationTime = engagement.CurrentSeconds;
        }

        public virtual void OnDrawGizmos()
        {
            
        }
    }
}