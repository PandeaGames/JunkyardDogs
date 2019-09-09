namespace JunkyardDogs.Simulation
{
    public class SimObject
    {
        protected SimulatedEngagement engagement;

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
        }

        public virtual void OnDrawGizmos()
        {
            
        }
    }
}