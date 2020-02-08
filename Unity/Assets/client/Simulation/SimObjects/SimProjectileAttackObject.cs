using JunkyardDogs.Components;
using JunkyardDogs.Specifications;
using UnityEngine;
using Chassis = JunkyardDogs.Components.Chassis;
using Weapon = JunkyardDogs.Components.Weapon;

namespace JunkyardDogs.Simulation
{
    public class SimProjectileAttackObject : SimPhysicalAttackObject
    {
        public ProjectileWeapon projectileWeapon;
        public Weapon Weapon;
        
        public SimProjectileAttackObject(SimulatedEngagement engagement, SimBot simBot, Chassis.ArmamentLocation armementLocation) : base(engagement, simBot, armementLocation)
        {
            Weapon = simBot.bot.GetArmament(armementLocation);
            projectileWeapon = (ProjectileWeapon)Weapon.GetSpec();
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