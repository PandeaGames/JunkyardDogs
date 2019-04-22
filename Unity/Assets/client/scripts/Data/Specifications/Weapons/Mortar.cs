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
    [CreateAssetMenu(fileName = "Mortar", menuName = "Specifications/Mortar", order = 6)]
    public class Mortar : Weapon, IStaticDataBalance<MortarWeaponBalanceObject>
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

        public void ApplyBalance(MortarWeaponBalanceObject balance)
        {
            name = balance.name;
            _volume = balance.volume;
            
#if UNITY_EDITOR
            if (_shell == null)
            {
                _shell = CreateInstance<MortarShell>();
                _shell.name = "Shell";
                AssetDatabase.AddObjectToAsset(_shell, AssetDatabase.GetAssetPath(this));
            }
#endif
            _shell.Radius = balance.radius;
            _shell.Damage = balance.damage;
            _cooldown = balance.cooldown;
            _chargeTime = balance.chargeTime;
            _shell.ApplyBalance(balance);
        }

        public MortarWeaponBalanceObject GetBalance()
        {
            throw new System.NotImplementedException();
        }
    }
}