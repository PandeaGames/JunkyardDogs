using UnityEngine;
using System.Collections;
using JunkyardDogs.Simulation;

namespace JunkyardDogs.Specifications
{
    [CreateAssetMenu(fileName = "PulseEmitter", menuName = "Specifications/PulseEmitter", order = 6)]
    public class PulseEmitter : Weapon
    {
        private Pulse _pulse;
        public Pulse Pulse { get { return _pulse; } }

        private float _speed;
        private float _radius;

        public float Speed { get { return _speed; } }
        public float Radius { get { return _radius; } }

        public override AttackActionResult GetResult()
        {
            AttackActionResult result = base.GetResult();
            AttackActionResult.Pulse pulseResult = result.PulseResult;

            pulseResult.Range = _radius;
            pulseResult.Velocity = Speed;

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