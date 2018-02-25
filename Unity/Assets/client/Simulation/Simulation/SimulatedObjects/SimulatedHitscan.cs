using UnityEngine;
using System.Collections;
using JunkyardDogs.Components;

namespace JunkyardDogs.Simulation.Simulation
{
    public class SimulatedHitscan : SimulatedAttack
    {
        public SimulatedHitscan(SimulatedBot bot, SimulatedBot opponent, AttackActionResult actionResult, WeaponProcessor processor) : base(bot, opponent, actionResult, processor)
        {
            body = new SimulatedBody();
            SimulatedLineCollider lineCollider = new SimulatedLineCollider(body);
            collider = lineCollider;

            if (opponent.body != null)
            {
                Vector2 delta = opponent.body.position - bot.body.position;

                body.position = bot.body.position;

                lineCollider.angle = Mathf.Atan2(delta.y, delta.x);
            }
        }
    }
}
