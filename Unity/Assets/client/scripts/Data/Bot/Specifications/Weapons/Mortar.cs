using UnityEngine;
using System.Collections;
using JunkyardDogs.Simulation;

namespace JunkyardDogs.Specifications
{
    [CreateAssetMenu(fileName = "Mortar", menuName = "Specifications/Mortar", order = 6)]
    public class Mortar : Weapon
    {
        private MortarShell _shell;

        public MortarShell Shell { get { return _shell; } }

        public override AttackActionResult GetResult()
        {
            AttackActionResult result = base.GetResult();
            AttackActionResult.Mortar mortarResult = result.MortarResult;

            result.DamageOuput = _shell.Damage;

            mortarResult.Velocity = _shell.Speed;
            mortarResult.Radius = _shell.Radius;

            return result;
        }

        public override Assailer GetAssailer()
        {
            return _shell;
        }

        public override AttackActionResultType GetActionType()
        {
            return AttackActionResultType.MORTAR;
        }
    }
}