using UnityEngine;
using System.Collections;
using JunkyardDogs.Data.Balance;
using JunkyardDogs.Simulation;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Weapon = JunkyardDogs.Specifications.Weapon;

namespace JunkyardDogs.Specifications
{
    [CreateAssetMenu(fileName = "PulseEmitter", menuName = "Specifications/PulseEmitter", order = 6)]
    public class PulseEmitter : Weapon, IStaticDataBalance<PulseWeaponBalanceObject>
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

        public void ApplyBalance(PulseWeaponBalanceObject balance)
        {
            name = balance.name;
            _radius = balance.radius;
            _speed = balance.speed;
            _volume = balance.volume;
            _cooldown = balance.cooldown;
            _chargeTime = balance.chargeTime;

            #if UNITY_EDITOR
            if (_pulse == null)
            {
                _pulse = CreateInstance<Pulse>();
                _pulse.name = "Pulse";
                AssetDatabase.AddObjectToAsset(_pulse, AssetDatabase.GetAssetPath(this));
            }
            
            #endif

            _pulse.Damage = balance.damage;
            
        }

        public PulseWeaponBalanceObject GetBalance()
        {
            PulseWeaponBalanceObject balance = new PulseWeaponBalanceObject();

            balance.speed = _speed;
            balance.damage = _pulse.Damage;
            balance.name = name;
            balance.radius = _radius;
            balance.cooldown = _cooldown;
            balance.chargeTime = _chargeTime;

            return balance;
        }
    }
}