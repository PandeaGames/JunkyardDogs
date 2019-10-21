using System;
using JunkyardDogs.Simulation;
using PandeaGames;
using UnityEngine;

public class SimpleEffects : ISimulatedEngagementEventHandler
{
    private SimpleSimulatedEngagement _viewContainer;
    public SimpleEffects(SimpleSimulatedEngagement viewContainer)
    {
        _viewContainer = viewContainer;
    }
    
    public void OnSimEvent(SimulatedEngagement engagement, SimEvent simEvent)
    {
        if (simEvent is SimCollisionEvent)
        {
            OnSimEvent(engagement, simEvent as SimCollisionEvent);
        }
    }
    
    public void OnSimEvent(SimulatedEngagement engagement, SimCollisionEvent simEvent)
    {
        SimPhysicalAttackObject obj = null;
        SimPhysicsObject otherObj = null;
        
        if (simEvent.obj1 is SimPhysicalAttackObject)
        {
            obj = simEvent.obj1 as SimPhysicalAttackObject;
            otherObj = simEvent.obj2;
        }
        else if(simEvent.obj2 is SimPhysicalAttackObject)
        {
            obj = simEvent.obj2 as SimPhysicalAttackObject;
            otherObj = simEvent.obj1;
        }

        if (obj != null)
        {
            OnPhysicalAttackCollision(obj, otherObj);
        }
    }

    private void OnPhysicalAttackCollision(SimPhysicalAttackObject obj, SimPhysicsObject otherObj)
    {
        if (obj is SimProjectileAttackObject && otherObj is SimArena)
        {
            SmallExplosionEffect(obj);
        }
        else if (obj is SimProjectileAttackObject  && obj.SimBot.opponent == otherObj)
        {
            ExplosionEffect(obj);
        }
        else if (obj is SimPulseAttack  && obj.SimBot.opponent == otherObj)
        {
            PulseExplosionEffect(otherObj);
        }
    }

    private void ExplosionEffect(SimPhysicsObject simObject)
    {
        GameObject.Instantiate(
            _viewContainer.botRenderConfiguration.ExplosionPrefab,
            new Vector3(simObject.body.position.x, 0, simObject.body.position.y), 
            Quaternion.identity, null);
    }
    
    private void SmallExplosionEffect(SimPhysicsObject simObject)
    {
        GameObject.Instantiate(_viewContainer.botRenderConfiguration.SmallExplosionPrefab,
            new Vector3(simObject.body.position.x, 0, simObject.body.position.y),
            Quaternion.identity, null);
    }
    
    private void PulseExplosionEffect(SimPhysicsObject simObject)
    {
        GameObject.Instantiate(_viewContainer.botRenderConfiguration.SmallPlasmaExplosionPrefab,
            new Vector3(simObject.body.position.x, 0, simObject.body.position.y),
            Quaternion.identity, null);
    }

    public Type[] EventsToHandle()
    {
        return new[] { typeof(SimCollisionEvent) };
    }
}
