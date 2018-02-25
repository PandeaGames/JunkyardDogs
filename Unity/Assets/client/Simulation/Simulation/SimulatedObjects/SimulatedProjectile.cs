using UnityEngine;
using System.Collections;
using JunkyardDogs.Components;

namespace JunkyardDogs.Simulation.Simulation
{
    public class SimulatedProjectile : SimulatedAttack
    {
        public SimulatedProjectile(SimulatedBot bot, SimulatedBot opponent, AttackActionResult actionResult, WeaponProcessor processor) : base(bot, opponent, actionResult, processor)
        {
            body = new SimulatedBody();
            SimulatedCircleCollider circleCollider = new SimulatedCircleCollider(body);
            collider = circleCollider;

            if (opponent.body != null)
            {
                Vector2 delta = opponent.body.position - bot.body.position;

                body.position = bot.body.position;
                circleCollider.radius = actionResult.ProjectileResult.Radius;

                float angle = Mathf.Atan2(delta.y, delta.x);

                body.velocity.Set(
                    Mathf.Cos(angle) * actionResult.ProjectileResult.Velocity,
                    Mathf.Sin(angle) * actionResult.ProjectileResult.Velocity
                    );
            }
        }
    }
}