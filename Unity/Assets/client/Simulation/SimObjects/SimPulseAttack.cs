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
            weapon = simBot.bot.GetArmament(armementLocation).GetSpec<PulseEmitter>();
            collider = CreateCollider(weapon);
            TimeInstantiated = engagement.CurrentSeconds;
        }

        public override void OnSimEvent(SimulatedEngagement engagement, SimPostLogicEvent simEvent)
        {
            base.OnSimEvent(engagement, simEvent);

            SimulatedCircleCollider circleCollider = collider as SimulatedCircleCollider;
            circleCollider.radius = 0.5f + (float)((engagement.CurrentSeconds - TimeInstantiated) * weapon.Speed);
            
            if (circleCollider.radius > weapon.Radius)
            {
                engagement.MarkForRemoval(this);
            }
        }
    }
}