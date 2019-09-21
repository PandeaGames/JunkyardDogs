using UnityEngine;
using System.Collections;
using JunkyardDogs.Data.Balance;
using JunkyardDogs.Simulation;

namespace JunkyardDogs.Specifications
{
    public abstract class Weapon : PhysicalSpecification, IStaticDataBalance<WeaponBalanceObject>
    {
        [SerializeField]
        protected double _chargeTime;

        [SerializeField]
        protected double _cooldown;

        [SerializeField]
        protected double _power;

        [SerializeField] 
        protected int _armourPiercing;

        [SerializeField] 
        protected float _stun;

        public double ChargeTime { get { return _chargeTime; } }
        public double Cooldown { get { return _cooldown; } }
        public virtual double Power { get { return _power; } }
        public virtual float Stun { get { return _stun; } }

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
        public void ApplyBalance(WeaponBalanceObject balance)
        {
            base.ApplyBalance(balance);
            _stun = balance.stun;
            _cooldown = balance.cooldown;
            _chargeTime = balance.chargeTime;
        }

        public WeaponBalanceObject GetBalance()
        {
            WeaponBalanceObject balance = new WeaponBalanceObject();

            balance.name = name;
            balance.cooldown = _cooldown;
            balance.chargeTime = _chargeTime;

            return balance;
        }
    }
}