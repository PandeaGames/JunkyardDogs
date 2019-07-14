using System;
using System.Collections.Generic;
using System.Linq;
using JunkyardDogs.Components;
using UnityEngine;
using UnityEngine.UI.Extensions;
using EventHandlersTable = System.Collections.Generic.Dictionary<System.Type, System.Collections.Generic.List<JunkyardDogs.Simulation.ISimulatedEngagementEventHandler>>;
using Random = UnityEngine.Random;

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
        private List<SimObject> ObjectsMarkedForInstantiation;
        private List<SimObject> ObjectsMarkedForRemoval;

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
            ObjectsMarkedForInstantiation = new List<SimObject>();
            ObjectsMarkedForRemoval = new List<SimObject>();
            
            _engagement = engagement;
            _listener = listener;
            _eventHandlers = GetEventHandlers();
        }

        public bool Step()
        {
            bool isSimulationComplete = _engagement.Outcome != null;
            if (!isSimulationComplete)
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
                
                    Add(botBlue);
                    Add(botRed);
                }
            
                SyncronizObjectsBeforeStep();
                _listener.StepStart();
                SendEvent(new SimLogicEvent());
                SendEvent(new SimPostLogicEvent());
                _listener.StepComplete();
                _step++;
                ProcessEndOfMatchOutcom();
                isSimulationComplete = _engagement.Outcome != null;
            }
            
            return isSimulationComplete;
        }

        private void ProcessEndOfMatchOutcom()
        {
            if (CurrentSeconds > _engagement.Rules.MatchTimeLimit)
            {
                int winnerChoice = Random.Range(0, 1);
                
                Bot winner = winnerChoice == 0 ? _engagement.RedCombatent:_engagement.BlueCombatent;
                Bot loser = _engagement.RedCombatent == winner ? _engagement.BlueCombatent : _engagement.RedCombatent;

                _engagement.Outcome = new SimulationService.Outcome(winner, loser, (float)CurrentSeconds);
            }
        }

        public void SendEvent(SimEvent simEvent)
        {
            Type eventType = simEvent.GetType();
            List<ISimulatedEngagementEventHandler> handlers = null;
            _eventHandlers.TryGetValue(eventType, out handlers);

            if (handlers != null)
            {
                foreach (ISimulatedEngagementEventHandler handler in handlers)
                {
                    handler.OnSimEvent(this, simEvent);
                }
            }
            
            _listener.OnEvent(this, simEvent);
            OnEvent(simEvent);
        }

        private void OnEvent(SimEvent simEvent)
        {
            Debug.Log(simEvent);
            if (simEvent is SimDamageTakenEvent)
            {
                OnEvent(simEvent as SimDamageTakenEvent);
            }
        }
        
        private void OnEvent(SimDamageTakenEvent simEvent)
        {
            if (simEvent.simBot.RemainingHealth <= 0)
            {
                _engagement.Outcome = new SimulationService.Outcome(simEvent.simBot.bot, simEvent.simBot.opponent.bot, (float)CurrentSeconds);
            }
        }

        private void SyncronizObjectsBeforeStep()
        {
            foreach (SimObject simObj in ObjectsMarkedForRemoval)
            {
                SendEvent(new SimDestroyEvent(simObj));
            
                if (simObj is ISimulatedEngagementEventHandler)
                {
                    RemoveEventHandler(_eventHandlers, simObj as ISimulatedEngagementEventHandler);
                }

                Objects.Remove(simObj);
            }
            
            foreach (SimObject simObj in ObjectsMarkedForInstantiation)
            {
                SendEvent(new SimInstantiationEvent(simObj));
            
                if (simObj is ISimulatedEngagementEventHandler)
                {
                    AddEventHandler(_eventHandlers, simObj as ISimulatedEngagementEventHandler);
                }
            }
            
            Objects.AddRange(ObjectsMarkedForInstantiation);
            ObjectsMarkedForRemoval.Clear();
            ObjectsMarkedForInstantiation.Clear();
        }

        public void Add(SimObject simObj)
        {
            ObjectsMarkedForInstantiation.Add(simObj);
        }
        
        public void MarkForRemoval(SimObject simObj)
        {
            ObjectsMarkedForRemoval.Add(simObj);
        }
    }
}