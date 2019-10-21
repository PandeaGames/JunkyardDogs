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
        
        [SerializeField]
        private float _radius;
        public float Radius { get { return _radius; } }

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
            base.ApplyBalance(balance);
            name = balance.name;
            _range = balance.range;
            _meleeWeapon.Damage = balance.damage;
            _meleeWeapon.Range = _range;
            _volume = balance.volume;
            _radius = balance.radius;
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
            balance.armourPiercing = _armourPiercing;
            balance.radius = _radius;
            
            return balance;
        }
    }
}