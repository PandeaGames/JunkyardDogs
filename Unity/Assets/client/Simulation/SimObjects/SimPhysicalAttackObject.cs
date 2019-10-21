using System;
using JunkyardDogs.Simulation.Simulation;
using JunkyardDogs.Specifications;
using UnityEngine;
using Chassis = JunkyardDogs.Components.Chassis;

namespace JunkyardDogs.Simulation
{
    public class SimPhysicalAttackObject : SimPhysicsObject, ISimAttack, ISimulatedEngagementEventHandler
    {
        protected SimBot simBot;
        private Chassis.ArmamentLocation armementLocation;
        protected bool removeOnCollision = true;

        public double Damage
        {
            get { return simBot.bot.GetArmament(armementLocation).GetSpec<Weapon>().Power; }
        }
        
        public float Knockback
        {
            get { return simBot.bot.GetArmament(armementLocation).GetSpec<Weapon>().Knockback; }
        }
        
        public SimPhysicalAttackObject(SimulatedEngagement engagement, SimBot simBot, Chassis.ArmamentLocation armementLocation) : base(engagement)
        {
            this.simBot = simBot;
            this.armementLocation = armementLocation;
            
        }

        public SimBot SimBot
        {
            get { return simBot; }
        }

        protected SimulatedCollider CreateCollider(Weapon weapon)
        {
            if (weapon is ProjectileWeapon)
            {
                return CreateCollider(weapon as ProjectileWeapon);
            }
            else if (weapon is Melee)
            {
                return CreateCollider(weapon as Melee);
            }
            else if (weapon is Hitscan)
            {
                return CreateCollider(weapon as Hitscan);
            }
            else if (weapon is Mortar)
            {
                return CreateCollider(weapon as Mortar);
            }
            else if (weapon is PulseEmitter)
            {
                return CreateCollider(weapon as PulseEmitter);
            }

            return null;
        }
        
        private SimulatedCollider CreateCollider(ProjectileWeapon weapon)
        {
            SimulatedCircleCollider collider = new SimulatedCircleCollider(body);
            collider.radius = weapon.Radius;
            return collider;
        }
        
        private SimulatedCollider CreateCollider(Melee weapon)
        {
            SimulatedCircleCollider collider = new SimulatedCircleCollider(body);
            collider.radius = weapon.Radius;
            return collider;
        }
        
        private SimulatedCollider CreateCollider(Hitscan weapon)
        {
            SimulatedLineCollider collider = new SimulatedLineCollider(body);
                
            if (simBot.opponent.body != null)
            {
                Vector2 delta = simBot.opponent.body.position - simBot.body.position;
                //collider.angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
                collider.angle = body.rotation.rad;
                //collider.angle = 45 * Mathf.Deg2Rad;
                //collider.angle = 225f * Mathf.Deg2Rad;
            }

            return collider;
        }
        
        private SimulatedCollider CreateCollider(Mortar weapon)
        {
            SimulatedCircleCollider collider = new SimulatedCircleCollider(body);
            collider.radius = weapon.Radius;
            return collider;
        }
        
        private SimulatedCollider CreateCollider(PulseEmitter weapon)
        {
            SimulatedCircleCollider collider = new SimulatedCircleCollider(body);
            collider.radius = 1f;
            return collider;
        }

        public virtual void OnSimEvent(SimulatedEngagement engagement, SimEvent simEvent)
        {
            if (simEvent is SimPostLogicEvent)
            {
                OnSimEvent(engagement, simEvent as SimPostLogicEvent);
            }
        }
        
        public virtual void OnSimEvent(SimulatedEngagement engagement, SimPostLogicEvent simEvent)
        {
            
        }

        public override void OnCollision(SimPhysicsObject other)
        {
            base.OnCollision(other);

            if (other == simBot.opponent && removeOnCollision)
            {
                simBot.opponent.Stun(simBot.bot.GetArmament(armementLocation).GetSpec<Weapon>().Stun);
                engagement.MarkForRemoval(this);
            }
        }

        public Type[] EventsToHandle()
        {
            return new Type[] { typeof(SimCollisionEvent), typeof(SimPostLogicEvent) };
        }
    }
}