using UnityEngine;
using System.Collections;
using JunkyardDogs.Data.Balance;
using JunkyardDogs.Simulation;
#if UNITY_EDITOR
using UnityEditor;    
#endif

namespace JunkyardDogs.Specifications
{
    [CreateAssetMenu(fileName = "Hitscan", menuName = "Specifications/Hitscan", order = 6)]
    public class Hitscan : Weapon, IStaticDataBalance<HitscanWeaponBalanceObject>
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

        public void ApplyBalance(HitscanWeaponBalanceObject balance)
        {
            _cooldown = balance.cooldown;
            _volume = balance.volume;
            _chargeTime = balance.chargeTime;
            _armourPiercing = balance.armourPiercing;
            name = balance.name;
            
#if UNITY_EDITOR
            if (_shell == null)
            {
                _shell = CreateInstance<HitscanBullet>();
                _shell.name = "Shell";
                AssetDatabase.AddObjectToAsset(_shell, AssetDatabase.GetAssetPath(this));
            }
#endif
            _shell.Damage = balance.damage;
        }

        public HitscanWeaponBalanceObject GetBalance()
        {
            HitscanWeaponBalanceObject balance = new HitscanWeaponBalanceObject();

            balance.name = name;
            balance.volume = _volume;
            balance.cooldown = _cooldown;
            balance.chargeTime = _chargeTime;
            balance.armourPiercing = _armourPiercing;

            if (_shell != null)
            {
                balance.damage = _shell.Damage;
            }
            
            return balance;
        }
    }
}