using System;
using System.Collections.Generic;
using System.Linq;
using JunkyardDogs.Components;
using UnityEngine;
using UnityEngine.UI.Extensions;
using EventHandlersTable = System.Collections.Generic.Dictionary<System.Type, System.Collections.Generic.List<JunkyardDogs.Simulation.ISimulatedEngagementEventHandler>>;

namespace JunkyardDogs.Simulation
{
    public interface ISimulatedEngagementListener
    {
        void StepStart();
        void StepComplete();
        void OnEvent(SimulatedEngagement simulatedEngagement, SimEvent e);
    }
    
    public interface ISimulatedEngagementEventHandler
    {
        void OnSimEvent(SimulatedEngagement engagement, SimEvent simEvent);
        Type[] EventsToHandle();
    }
    
    public interface ISimulatedEngagementGlobalEventHandler : ISimulatedEngagementEventHandler
    {
    }
    
    public class SimulatedEngagement
    {
        public const float SimuationStep = 1f / 30f;
        
        private Engagement _engagement;
        private ISimulatedEngagementListener _listener;
        private int _step;
        public int CurrentStep
        {
            get { return _step; }
        }

        public double CurrentSeconds
        {
            get { return ConvertStepsToSeconds(CurrentStep); }
        }

        public List<SimObject> Objects;

        private EventHandlersTable _eventHandlers;

        public double ConvertStepsToSeconds(int steps)
        {
            return steps * SimuationStep;
        }

        public static EventHandlersTable GetEventHandlers()
        {
            EventHandlersTable eventHandlers = new EventHandlersTable();
            Type type = typeof(ISimulatedEngagementGlobalEventHandler);
            IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && type.IsClass);

            foreach (Type foundType in types)
            {
                ISimulatedEngagementGlobalEventHandler handlerInstance = (ISimulatedEngagementGlobalEventHandler)Activator.CreateInstance(foundType);
                AddEventHandler(eventHandlers, handlerInstance);
            }

            return eventHandlers;
        }

        private static void AddEventHandler(EventHandlersTable eventHandlers, ISimulatedEngagementEventHandler handlerInstance)
        {
            Type[] eventsToHandle = handlerInstance.EventsToHandle();

            foreach (Type eventToHandle in eventsToHandle)
            {
                List<ISimulatedEngagementEventHandler> handlers = null;
                eventHandlers.TryGetValue(eventToHandle, out handlers);
                    
                if (handlers == null)
                {
                    handlers = new List<ISimulatedEngagementEventHandler>();
                    eventHandlers.Add(eventToHandle, handlers);
                }
                    
                handlers.Add(handlerInstance);
            }
        }
        
        private static void RemoveEventHandler(EventHandlersTable eventHandlers, ISimulatedEngagementEventHandler handlerInstance)
        {
            Type[] eventsToHandle = handlerInstance.EventsToHandle();

            foreach (Type eventToHandle in eventsToHandle)
            {
                List<ISimulatedEngagementEventHandler> handlers = null;
                eventHandlers.TryGetValue(eventToHandle, out handlers);
                    
                if (handlers != null)
                {
                    handlers.Remove(handlerInstance);
                }
            }
        }
        
        public SimulatedEngagement(Engagement engagement, ISimulatedEngagementListener listener)
        {
            Objects = new List<SimObject>();
            _engagement = engagement;
            _listener = listener;
            _eventHandlers = GetEventHandlers();
        }

        public bool Step()
        {
            bool isFirstStep = _step == 0;
            if (isFirstStep)
            {
                SimBot botRed = new SimBot(this);
                SimBot botBlue = new SimBot(this);
                
                botRed.bot = _engagement.RedCombatent;
                botBlue.bot = _engagement.BlueCombatent;
                
                botRed.opponent = botBlue;
                botBlue.opponent = botRed;
                
                Instantiate(botBlue);
                Instantiate(botRed);
            }
            
            bool isSimulationComplete = false;
            _listener.StepStart();
            SendEvent(new SimLogicEvent());
            SendEvent(new SimPostLogicEvent());
            _listener.StepComplete();
            _step++;
            return isSimulationComplete;
        }

        public void SendEvent(SimEvent simEvent)
        {
            List<ISimulatedEngagementEventHandler> handlers = null;
            _eventHandlers.TryGetValue(typeof(SimEvent), out handlers);

            if (handlers != null)
            {
                foreach (ISimulatedEngagementEventHandler handler in handlers)
                {
                    handler.OnSimEvent(this, simEvent);
                }
            }
            
            _listener.OnEvent(this, simEvent);
        }

        public void Instantiate(SimObject simObj)
        {
            Objects.Add(simObj);
            SendEvent(new SimInstantiationEvent(simObj));
            
            if (simObj is ISimulatedEngagementEventHandler)
            {
                AddEventHandler(_eventHandlers, simObj as ISimulatedEngagementEventHandler);
            }
        }
        
        public void Destroy(SimObject simObj)
        {
            Objects.Remove(simObj);
            SendEvent(new SimDestroyEvent(simObj));
            
            if (simObj is ISimulatedEngagementEventHandler)
            {
                RemoveEventHandler(_eventHandlers, simObj as ISimulatedEngagementEventHandler);
            }
        }
    }
}