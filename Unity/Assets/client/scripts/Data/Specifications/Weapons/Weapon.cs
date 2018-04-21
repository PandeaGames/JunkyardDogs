using UnityEngine;
using System.Collections;
using JunkyardDogs.Simulation;

namespace JunkyardDogs.Specifications
{
    public abstract class Weapon : PhysicalSpecification
    {
        [SerializeField]
        private double _chargeTime;

        [SerializeField]
        private double _cooldown;

        public double ChargeTime { get { return _chargeTime; } }
        public double Cooldown { get { return _cooldown; } }

        public virtual AttackActionResult GetResult()
        {
            AttackActionResult result = default(AttackActionResult);

            result.ChargeTime = _chargeTime;
            result.RecoveryTime = Cooldown;
            result.Type = GetActionType();

            return result;
        }

        public abstract Assailer GetAssailer();
        public abstract AttackActionResultType GetActionType();
    }
}