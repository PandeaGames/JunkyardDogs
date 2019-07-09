namespace JunkyardDogs.Simulation
{
    public class SimEvent
    {
        
    }
    
    public abstract class SimInstanceEvent : SimEvent
    {
        public SimObject instance;

        public SimInstanceEvent(SimObject instance)
        {
            this.instance = instance;
        }
    }

    public class SimInstantiationEvent : SimInstanceEvent
    {
        public SimInstantiationEvent(SimObject instance) : base(instance) { }
    }
    
    public class SimDestroyEvent : SimInstanceEvent
    {
        public SimDestroyEvent(SimObject instance) : base(instance) { }
    }

    public class SimLogicEvent : SimEvent
    {
        
    }
    
    public class SimPostLogicEvent : SimEvent
    {
        
    }
}