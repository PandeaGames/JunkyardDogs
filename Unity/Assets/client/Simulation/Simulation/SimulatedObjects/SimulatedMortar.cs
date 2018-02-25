using UnityEngine;
using System.Collections;
using JunkyardDogs.Components;

namespace JunkyardDogs.Simulation.Simulation
{
    public class SimulatedMortar : SimulatedAttack
    {
        public SimulatedMortar(SimulatedBot bot, SimulatedBot opponent, AttackActionResult actionResult, WeaponProcessor processor) : base(bot, opponent, actionResult, processor)
        {
        }
    }
}