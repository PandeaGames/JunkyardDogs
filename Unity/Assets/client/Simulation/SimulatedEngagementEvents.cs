using JunkyardDogs.Components;

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
    
    public abstract class SimBotEvent : SimEvent
    {
        public SimBot simBot;

        public SimBotEvent(SimBot simBot)
        {
            this.simBot = simBot;
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

    public class SimDamageTakenEvent : SimBotEvent
    {
        public readonly double damageTaken;
        public SimDamageTakenEvent(SimBot simBot, double damageTaken) : base(simBot)
        {
            this.damageTaken = damageTaken;
        }
    }
}