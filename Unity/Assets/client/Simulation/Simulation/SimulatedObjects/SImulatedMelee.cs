using UnityEngine;
using System.Collections;
using JunkyardDogs.Components;

namespace JunkyardDogs.Simulation.Simulation
{
    public class SimulatedMelee : SimulatedAttack
    {
        public SimulatedMelee(SimulatedBot bot, SimulatedBot opponent, AttackActionResult actionResult, WeaponProcessor processor) : base(bot, opponent, actionResult, processor)
        {
        }
    }
}