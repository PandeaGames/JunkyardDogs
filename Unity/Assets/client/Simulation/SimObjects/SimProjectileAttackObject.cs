using JunkyardDogs.Specifications;
using UnityEngine;
using Chassis = JunkyardDogs.Components.Chassis;

namespace JunkyardDogs.Simulation
{
    public class SimProjectileAttackObject : SimPhysicalAttackObject
    {
        private ProjectileWeapon projectileWeapon;
        
        public SimProjectileAttackObject(SimulatedEngagement engagement, SimBot simBot, Chassis.ArmamentLocation armementLocation) : base(engagement, simBot, armementLocation)
        {
            projectileWeapon = simBot.bot.GetArmament(armementLocation).GetSpec<ProjectileWeapon>();
            collider = CreateCollider(projectileWeapon);
            body.rotation = simBot.body.rotation;
            body.velocityPerSecond = new Vector2(0, projectileWeapon.Speed);
        }
    }
}