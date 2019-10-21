using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JunkyardDogs.Components;
using UnityEditor;
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

        public Engagement Engagement
        {
            get { return _engagement; }
        }

        public double CurrentSeconds
        {
            get { return ConvertStepsToSeconds(CurrentStep); }
        }

        private int instanceCount;
        public List<SimObject> Objects;
        private List<SimObject> ObjectsMarkedForInstantiation;
        private HashSet<SimObject> ObjectsMarkedForRemoval;
        public List<SimObject> ObjectHistory;

        private EventHandlersTable _eventHandlers;

        public double ConvertStepsToSeconds(int steps)
        {
            return steps * SimuationStep;
        }

        public static EventHandlersTable GetEventHandlers()
        {
            EventHandlersTable eventHandlers = new EventHandlersTable();
            Type type = typeof(ISimulatedEngagementGlobalEventHandler);
            
            Assembly[] asemblies = AppDomain.CurrentDomain.GetAssemblies();
            List<Type> foundDecisionMakerTypes = new List<Type>();
                    
            foreach (Assembly assembly in asemblies)
            {
                Type[] typesInAssembly = assembly.GetTypes();

                foreach (Type typeInAssembly in typesInAssembly)
                {
                    bool IsAssignableFrom = type.IsAssignableFrom(typeInAssembly);
                    bool isGlobalEventHandler = IsAssignableFrom;
                    isGlobalEventHandler &= typeInAssembly.IsClass;
                    isGlobalEventHandler &= !typeInAssembly.IsAbstract;

                    if (isGlobalEventHandler)
                    {
                        ISimulatedEngagementGlobalEventHandler handlerInstance = (ISimulatedEngagementGlobalEventHandler)Activator.CreateInstance(typeInAssembly);
                        AddEventHandler(eventHandlers, handlerInstance);
                        foundDecisionMakerTypes.Add(typeInAssembly);
                    }
                }
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
            ObjectsMarkedForRemoval = new HashSet<SimObject>();
            ObjectHistory = new List<SimObject>();
            
            _engagement = engagement;
            _listener = listener;
            _eventHandlers = GetEventHandlers();
            
            _engagement.Arena = new Arena();
            _engagement.Arena.dimensions = new Vector2(38, 21);
        }

        public void AddEventHandler(ISimulatedEngagementEventHandler handler)
        {
            AddEventHandler(_eventHandlers, handler);
        }
        
        public void RemoveEventHandler(ISimulatedEngagementEventHandler handler)
        {
            RemoveEventHandler(_eventHandlers, handler);
        }
        
        public void OnDrawGizmos()
        {
            foreach (SimObject simObject in Objects)
            {
                simObject.OnDrawGizmos();
            }

            Arena arena = _engagement.Arena;
            //squeedgie stepped on my keyboard
           // hhhhhhhhhhhhhhhbjkkkkkkkkkkkkkkkkkkk
           Gizmos.DrawCube(
               new Vector3(arena.Width / 2, 0, 0),
               new Vector3(0.3f, 3, arena.Height)
               );
           
           Gizmos.DrawCube(
               new Vector3(arena.Width / 2 * -1, 0, 0),
               new Vector3(0.3f, 3, arena.Height)
           );
           
           Gizmos.DrawCube(
               new Vector3(0, 0, arena.Height / 2 * -1),
               new Vector3(arena.Width, 3, 0.3f)
           );
           
           Gizmos.DrawCube(
               new Vector3(0, 0, arena.Height / 2),
               new Vector3(arena.Width, 3, 0.3f)
           );
        }

        public bool Step()
        {
            bool isSimulationComplete = _engagement.Outcome != null;
            if (!isSimulationComplete)
            {
                bool isFirstStep = _step == 0;
                if (isFirstStep)
                {
                    SimArena simArena = new SimArena(this, _engagement.Arena);
                    
                    SimBot botRed = new SimBot(this, Initiator.RED);
                    SimBot botBlue = new SimBot(this, Initiator.BLUE);
                
                    botRed.bot = _engagement.RedCombatent;
                    botBlue.bot = _engagement.BlueCombatent;
                
                    botRed.opponent = botBlue;
                    botBlue.opponent = botRed;
                    
                    botRed.body.position = new Vector2(-5, 0);
                    botBlue.body.position = new Vector2(5, 0);
                
                    Add(botBlue);
                    Add(botRed);
                    Add(simArena);
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
            if (simEvent is SimDamageTakenEvent)
            {
                OnEvent(simEvent as SimDamageTakenEvent);
            }
        }
        
        private void OnEvent(SimDamageTakenEvent simEvent)
        {
            if (simEvent.simBot.RemainingHealth <= 0)
            {
                _engagement.Outcome = new SimulationService.Outcome(simEvent.simBot.opponent.bot, simEvent.simBot.bot, (float)CurrentSeconds);
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
            ObjectHistory.AddRange(ObjectsMarkedForInstantiation);
            ObjectsMarkedForRemoval.Clear();
            ObjectsMarkedForInstantiation.Clear();
        }

        public void Add(SimObject simObj)
        {
            ObjectsMarkedForInstantiation.Add(simObj);
        }

        public void MarkForRemoval(SimObject simObj)
        {
            if (!ObjectsMarkedForRemoval.Contains(simObj))
            {
                ObjectsMarkedForRemoval.Add(simObj);
            }
        }
    }
}