namespace JunkyardDogs.Simulation
{
    public class SimulationRunner : ISimulatedEngagementListener
    {
        private SimulationRecording _recording;
        public SimulationRecording Recording
        {
            get { return _recording; }
        }
        private Engagement _engagement;
        
        public static void Run(Engagement engagement)
        {
            SimulationRunner runner = new SimulationRunner(engagement);
        }

        public SimulationRunner(Engagement engagement)
        {
            _engagement = engagement;
            SimulatedEngagement simulatedEngagement = new SimulatedEngagement(_engagement, this);
            while (simulatedEngagement.Step());
        }

        public void StepStart()
        {
            
        }

        public void StepComplete()
        {
            
        }

        public void OnEvent(SimulatedEngagement simulatedEngagement, SimEvent e)
        {
            _recording.AddEvent(simulatedEngagement.CurrentStep, e);
        }
    }
}