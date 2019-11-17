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
            projectileWeapon = (ProjectileWeapon)simBot.bot.GetArmament(armementLocation).GetSpec();
            colliders.Add(CreateCollider(projectileWeapon));
            body.rotation = simBot.body.rotation;
            body.accelerationPerSecond = new Vector2(0, projectileWeapon.Speed);
            body.isTrigger = true;
        }

        public override void OnCollision(SimPhysicsObject other)
        {
            base.OnCollision(other);

            if (other is SimArena)
            {
                engagement.MarkForRemoval(this);
            }
        }
    }
}