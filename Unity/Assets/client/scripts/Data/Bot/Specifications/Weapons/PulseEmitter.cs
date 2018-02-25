using UnityEngine;
using System.Collections;
using JunkyardDogs.Simulation;

namespace JunkyardDogs.Specifications
{
    [CreateAssetMenu(fileName = "PulseEmitter", menuName = "Specifications/PulseEmitter", order = 6)]
    public class PulseEmitter : Weapon
    {
        [SerializeField]
        private Pulse _pulse;
        public Pulse Pulse { get { return _pulse; } }

        [SerializeField]
        private float _speed;

        [SerializeField]
        private float _radius;

        public float Speed { get { return _speed; } }
        public float Radius { get { return _radius; } }

        public override AttackActionResult GetResult()
        {
            AttackActionResult result = base.GetResult();
            AttackActionResult.Pulse pulseResult = result.PulseResult;

            pulseResult.Range = _radius;
            pulseResult.Velocity = Speed;

            result.DamageOuput = _pulse.Damage;

            result.PulseResult = pulseResult;

            return result;
        }

        public override Assailer GetAssailer()
        {
            return _pulse;
        }

        public override AttackActionResultType GetActionType()
        {
            return AttackActionResultType.PULSE;
        }
    }
}