using System;
using JunkyardDogs.Components;
using JunkyardDogs.Simulation.Simulation;
using JunkyardDogs.Specifications;
using UnityEngine;
using Weapon = JunkyardDogs.Specifications.Weapon;

namespace JunkyardDogs.Simulation
{
    public class SimulatedAttacks : ISimulatedEngagementGlobalEventHandler
    {
        public void OnSimEvent(SimulatedEngagement engagement, SimEvent simEvent)
        {
            if (simEvent is WeaponDecisionEvent)
            {
                OnSimEvent(engagement, simEvent as WeaponDecisionEvent);
            }
        }

        public Type[] EventsToHandle()
        {
            return new Type[] { typeof(WeaponFireDecisionEvent) };
        }

        public void OnSimEvent(SimulatedEngagement engagement, WeaponDecisionEvent simEvent)
        {
            FireWeapon(engagement, simEvent.simBot, simEvent.WeaponProcessor, simEvent);
        }

        private void FireWeapon(SimulatedEngagement engagement, SimBot simBot, WeaponProcessor weaponProcessor, WeaponDecisionEvent simEvent)
        {
            Components.Weapon weapon = weaponProcessor.Weapon;

            if (weapon != null)
            {
                if (weapon.IsSpec<MeleeWeapon>())
                {
                    FireWeapon(engagement, simBot, weaponProcessor, weapon.GetSpec<MeleeWeapon>(), simEvent);
                }
                else if (weapon.IsSpec<ProjectileWeapon>())
                {
                    FireWeapon(engagement, simBot, weaponProcessor, weapon.GetSpec<ProjectileWeapon>(), simEvent);
                }
                else if (weapon.IsSpec<PulseEmitter>())
                {
                    FireWeapon(engagement, simBot, weaponProcessor, weapon.GetSpec<PulseEmitter>(), simEvent);
                }
                else if (weapon.IsSpec<Mortar>())
                {
                    FireWeapon(engagement, simBot, weaponProcessor, weapon.GetSpec<Mortar>(), simEvent);
                }
                else if (weapon.IsSpec<Hitscan>())
                {
                    FireWeapon(engagement, simBot, weaponProcessor, weapon.GetSpec<Hitscan>(), simEvent);
                }
            }
        }
        
        private void FireWeapon(SimulatedEngagement engagement, SimBot simBot, WeaponProcessor weaponProcessor, MeleeWeapon meleeWeapon, WeaponDecisionEvent simEvent)
        {
            SimMeleeAttack attack = new SimMeleeAttack(engagement, simBot, simEvent.armamentLocation);
            
            SimulatedBody body = simBot.body;
            Rect bounds = simBot.GetBounds();
            float radius = bounds.width / 2;
            float rotation = body.rotation.deg;
            
            Vector2 delta = new Vector2(Mathf.Cos(rotation) * radius, Mathf.Sin(rotation) * radius);
            attack.body.position = simBot.body.position + delta;
            engagement.Add(attack);
        }
        
        private void FireWeapon(SimulatedEngagement engagement, SimBot simBot, WeaponProcessor weaponProcessor, ProjectileWeapon meleeWeapon, WeaponDecisionEvent simEvent)
        {
            SimProjectileAttackObject projectile = new SimProjectileAttackObject(engagement, simBot, simEvent.armamentLocation);
            projectile.body.position = simBot.body.position;
            engagement.Add(projectile);
        }
        
        private void FireWeapon(SimulatedEngagement engagement, SimBot simBot, WeaponProcessor weaponProcessor, PulseEmitter meleeWeapon, WeaponDecisionEvent simEvent)
        {
            SimPulseAttack projectile = new SimPulseAttack(engagement, simBot, simEvent.armamentLocation);
            projectile.body.position = simBot.body.position;
            engagement.Add(projectile);
        }
        
        private void FireWeapon(SimulatedEngagement engagement, SimBot simBot, WeaponProcessor weaponProcessor, Mortar meleeWeapon, WeaponDecisionEvent simEvent)
        {
            SimMortarAttackObject mortar = new SimMortarAttackObject(engagement, simBot, simEvent.armamentLocation);
            mortar.body.position = simBot.body.position;
            engagement.Add(mortar);
        }
        
        private void FireWeapon(SimulatedEngagement engagement, SimBot simBot, WeaponProcessor weaponProcessor, Hitscan meleeWeapon, WeaponDecisionEvent simEvent)
        {
            SimHitscanShot mortar = new SimHitscanShot(engagement, simBot, simEvent.armamentLocation);
            mortar.body.position = simBot.body.position;
            engagement.Add(mortar);
        }
    }
}