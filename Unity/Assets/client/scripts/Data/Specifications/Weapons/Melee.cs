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
    [CreateAssetMenu(fileName = "Melee", menuName = "Specifications/Melee", order = 6)]
    public class Melee : Weapon, IStaticDataBalance<MeleeWeaponBalanceObject>
    {
        [SerializeField]
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
        
        public void ApplyBalance(MeleeWeaponBalanceObject balance)
        {
            name = balance.name;
            _range = balance.range;
            _cooldown = balance.cooldown;
            _chargeTime = balance.chargeTime;
            _meleeWeapon.Damage = balance.damage;
            _volume = balance.volume;
        }
        
        public MeleeWeaponBalanceObject GetBalance()
        {
            MeleeWeaponBalanceObject balance = new MeleeWeaponBalanceObject();
            
#if UNITY_EDITOR
            if (_meleeWeapon == null)
            {
                _meleeWeapon = ScriptableObject.CreateInstance<MeleeWeapon>();
                _meleeWeapon.name = "Assailer";
                AssetDatabase.AddObjectToAsset(_meleeWeapon, AssetDatabase.GetAssetPath(this));
            }
#endif

            balance.damage = _meleeWeapon.Damage;
            balance.cooldown = Cooldown;
            balance.chargeTime = ChargeTime;
            balance.volume = _volume;
            balance.name = name;
            balance.range = _range;
            
            return balance;
        }
    }
}