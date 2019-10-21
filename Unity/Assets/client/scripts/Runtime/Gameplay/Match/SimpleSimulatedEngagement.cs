using System;
using System.Collections.Generic;
using System.Net.Configuration;
using JunkyardDogs.scripts.Runtime.Gameplay.Match;
using JunkyardDogs.Simulation;
using UnityEngine;
using Object = System.Object;

public class SimpleSimulatedEngagement : MonoBehaviour, ISimulatedEngagementEventHandler
{
    [SerializeField]
    public BotRenderConfiguration botRenderConfiguration;
    
    [SerializeField]
    public PrefabFactory botPrefabFactory;

    [SerializeField] private SimpleCameraSystem _cameraSystem;

    private Dictionary<SimObject, SimpleSimulatedObjectView> _objects;

    private List<ISimulatedEngagementEventHandler> _eventHandlers;
    
    private void Start()
    {
        _eventHandlers = new List<ISimulatedEngagementEventHandler>();
        _objects = new Dictionary<SimObject, SimpleSimulatedObjectView>();
        _eventHandlers.Add(new SimpleEffects(this));
        _eventHandlers.Add(_cameraSystem);
    }

    private void Update()
    {
        foreach (KeyValuePair<SimObject, SimpleSimulatedObjectView> kvp in _objects)
        {
            kvp.Value.Update();
        }

        
    }
    
    public void OnSimEvent(SimulatedEngagement engagement, SimEvent simEvent)
    {
        foreach (ISimulatedEngagementEventHandler handler in _eventHandlers)
        {
            handler.OnSimEvent(engagement, simEvent);
        }
        if (simEvent is SimInstantiationEvent)
        {
            OnSimEvent(engagement, simEvent as SimInstantiationEvent);
        }
        else if (simEvent is SimDestroyEvent)
        {
            OnSimEvent(engagement, simEvent as SimDestroyEvent);
        }
    }

    public Type[] EventsToHandle()
    {
        return new Type[] { };
    }

    private void OnSimEvent(SimulatedEngagement engagement, SimDestroyEvent simEvent)
    {
        bool containsInstance = _objects.ContainsKey(simEvent.instance);
        if (containsInstance)
        {
            SimpleSimulatedObjectView view = _objects[simEvent.instance];
            view.Destroy();
            _objects.Remove(simEvent.instance);
        }
    }
    private void OnSimEvent(SimulatedEngagement engagement, SimInstantiationEvent simEvent)
    {
        if (simEvent.instance is SimBot)
        {
            SimBot simBot = simEvent.instance as SimBot;
            SimpleSimulatedBotView simBotView = new SimpleSimulatedBotView(this, simBot);
            _objects.Add(simBot, simBotView);
            simBotView.Make();
        }
        else if(simEvent.instance is SimProjectileAttackObject)
        {
            SimpleProjectileObjectView view = new SimpleProjectileObjectView(this, simEvent.instance as SimProjectileAttackObject);
            _objects.Add(simEvent.instance, view);
            view.Make();
        }
        else if(simEvent.instance is SimPulseAttack)
        {
            SimplePulseObjectView view = new SimplePulseObjectView(this, simEvent.instance as SimPulseAttack);
            _objects.Add(simEvent.instance, view);
            view.Make();
        }
        else if(simEvent.instance is SimMeleeAttack)
        {
            SimpleMeleeAttackView view = new SimpleMeleeAttackView(this, simEvent.instance as SimMeleeAttack);
            _objects.Add(simEvent.instance, view);
            view.Make();
        }
        
        else if(simEvent.instance is SimHitscanShot)
        {
            SimpleLaser view = new SimpleLaser(this, simEvent.instance as SimHitscanShot);
            SimpleMachineGun mgView =  new SimpleMachineGun(this, simEvent.instance as SimHitscanShot);
            _objects.Add(simEvent.instance, mgView);
            mgView.Make();
        }
    }

    private void OnDestroy()
    {
        foreach (KeyValuePair<SimObject, SimpleSimulatedObjectView> kvp in _objects)
        {
            kvp.Value.Destroy();
        }
        
        _objects.Clear();
        _objects = null;
    }
}
