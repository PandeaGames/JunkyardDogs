using UnityEngine;
using System.Collections;
using JunkyardDogs.Simulation;

namespace JunkyardDogs.Specifications
{
    [CreateAssetMenu(fileName = "Hitscan", menuName = "Specifications/Hitscan", order = 6)]
    public class Hitscan : Weapon
    {
        [SerializeField]
        private HitscanBullet _shell;

        public HitscanBullet Shell { get { return _shell; } }

        public override Assailer GetAssailer()
        {
            return _shell;
        }

        public override AttackActionResult GetResult()
        {
            AttackActionResult result = base.GetResult();
            AttackActionResult.Hitscan histscanResult = result.HitspanResult;

            result.DamageOuput = _shell.Damage;

            result.HitspanResult = histscanResult;

            return result;
        }

        public override AttackActionResultType GetActionType()
        {
            return AttackActionResultType.HITSCAN;
        }
    }
}