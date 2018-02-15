using UnityEngine;
using System.Collections;
using JunkyardDogs.Simulation;

namespace JunkyardDogs.Specifications
{
    [CreateAssetMenu(fileName = "Melee", menuName = "Specifications/Melee", order = 6)]
    public class Melee : Weapon
    {
        private MeleeWeapon _meleeWeapon;

        public MeleeWeapon MeleeWeapon { get { return _meleeWeapon; } }

        private int _range;
        public int Range { get { return _range; } }

        public override AttackActionResult GetResult()
        {
            AttackActionResult result = base.GetResult();
            AttackActionResult.Melee meleeResult = result.MeleeResult;

            result.DamageOuput = _meleeWeapon.Damage;

            meleeResult.range = _range;

            return result;
        }

        public override Assailer GetAssailer()
        {
            return _meleeWeapon;
        }

        public override AttackActionResultType GetActionType()
        {
            return AttackActionResultType.MELEE;
        }
    }
}