using System;

namespace JunkyardDogs.Simulation
{
    public class SimulatedCollisions : ISimulatedEngagementGlobalEventHandler
    {
        public void OnSimEvent(SimulatedEngagement engagement, SimEvent simEvent)
        {
            if (simEvent is SimCollisionEvent)
            {
                OnSimEvent(engagement, simEvent as SimCollisionEvent);
            }
        }
        
        public void OnSimEvent(SimulatedEngagement engagement, SimCollisionEvent simEvent)
        {
            simEvent.obj1.OnCollision(simEvent.obj2);
            simEvent.obj2.OnCollision(simEvent.obj1);
        }

        public Type[] EventsToHandle()
        {
            return new Type[] { typeof(SimCollisionEvent) };
        }
    }
}