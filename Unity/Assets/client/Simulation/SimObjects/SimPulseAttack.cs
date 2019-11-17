using JunkyardDogs.Simulation.Simulation;
using JunkyardDogs.Specifications;
using UnityEngine;
using Chassis = JunkyardDogs.Components.Chassis;

namespace JunkyardDogs.Simulation
{
    public class SimPulseAttack : SimPhysicalAttackObject
    {
        private PulseEmitter weapon;
        private double TimeInstantiated;
        
        public SimPulseAttack(SimulatedEngagement engagement, SimBot simBot, Chassis.ArmamentLocation armementLocation) : base(engagement, simBot, armementLocation)
        {
            weapon = (PulseEmitter) simBot.bot.GetArmament(armementLocation).GetSpec();
            colliders.Add(CreateCollider(weapon));
            TimeInstantiated = engagement.CurrentSeconds;
            body.isTrigger = true;
        }

        public override void OnSimEvent(SimulatedEngagement engagement, SimPostLogicEvent simEvent)
        {
            base.OnSimEvent(engagement, simEvent);
            float radius = (float) ((engagement.CurrentSeconds - TimeInstantiated) * weapon.Speed);
           // float scale = 0.5f + (float) ((engagement.CurrentSeconds - TimeInstantiated) * weapon.Speed);
            body.scale = new Vector2(radius, radius);
            
            if (radius > weapon.Radius)
            {
                engagement.MarkForRemoval(this);
            }
        }
    }
}