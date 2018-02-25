using UnityEngine;
using System.Collections;
using JunkyardDogs.Components;

namespace JunkyardDogs.Simulation.Simulation
{
    public class SimulatedAttack : SimulatedObject
    {
        private SimulatedBot _bot;
        private SimulatedBot _opponent;
        private WeaponProcessor _processor;
        private AttackActionResult _actionResult;

        public SimulatedBot Bot { get { return _bot; } }
        public WeaponProcessor Processor { get { return _processor; } }
        public AttackActionResult ActionResult { get { return _actionResult; } }

        public SimulatedAttack(SimulatedBot bot, SimulatedBot opponent, AttackActionResult actionResult, WeaponProcessor processor)
        {
            _bot = bot;
            _opponent = opponent;
            _actionResult = actionResult;
            _processor = processor;
        }

        public override void OnCollide(SimulatedObject other)
        {
            SimulatedBot simulatedBot = other as SimulatedBot;

            if (simulatedBot != null && other == _opponent)
            {
                simulatedBot.RecieveAttack(this);
                Remove();
            }
        }
    }
}
