namespace JunkyardDogs.Simulation
{
    public class SimAttackObject : SimObject, ISimAttack
    {
        private SimBot simBot;
        
        public SimAttackObject(SimulatedEngagement engagement, SimBot simBot) : base(engagement)
        {
        }

        public SimBot SimBot
        {
            get { return simBot; }
        }
    }
}