using UnityEngine;
using System.Collections;
using JunkyardDogs.Components;

namespace JunkyardDogs.Simulation.Simulation
{
    public class SimulatedPulse : SimulatedAttack
    {
        public float PulseVelocity = 0;
        private SimulatedCircleCollider _circleCollider;

        public SimulatedPulse(SimulatedBot bot, SimulatedBot opponent, AttackActionResult actionResult, WeaponProcessor processor) : base(bot, opponent, actionResult, processor)
        {
            PulseVelocity = actionResult.PulseResult.Velocity;

            body = new SimulatedBody();
            _circleCollider = new SimulatedCircleCollider(body);
            collider = _circleCollider;

            body.position = bot.body.position;

            _circleCollider.gizmosColor.a = 0.5f;
        }

        public override void Update()
        {
            _circleCollider.radius += PulseVelocity * SimulationService.SimuationStep;
        }
    }
}