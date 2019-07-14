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
    [CreateAssetMenu(fileName = "ProjectileWeapon", menuName = "Specifications/ProjectileWeapon", order = 6)]
    public class ProjectileWeapon : Weapon, IStaticDataBalance<ProjectileWeaponBalanceObject>
    {
        [SerializeField]
        private Bullet _shell;
        public Bullet Shell { get { return _shell; } }
        
        public float Speed { get { return _shell.Speed; } }
        public float Radius { get { return _shell.Radius; } }

        public override AttackActionResult GetResult()
        {
            AttackActionResult result = base.GetResult();
            AttackActionResult.Projectile projectileResult = result.ProjectileResult;

            result.DamageOuput = _shell.Damage;

            projectileResult.Velocity = _shell.Speed;
            projectileResult.Radius = _shell.Radius;

            result.ProjectileResult = projectileResult;

            return result;
        }

        public override Assailer GetAssailer()
        {
            return _shell;
        }

        public override AttackActionResultType GetActionType()
        {
            return AttackActionResultType.PROJECTILE;
        }

        public void ApplyBalance(ProjectileWeaponBalanceObject balance)
        {
            _volume = balance.volume;
            name = balance.name;

            #if UNITY_EDITOR
            if (_shell == null)
            {
                _shell = ScriptableObject.CreateInstance<Bullet>();
                _shell.name = "Shell";
                AssetDatabase.AddObjectToAsset(_shell, AssetDatabase.GetAssetPath(this));
            }
            #endif
            
            _shell.ApplyBalance(balance);
            _volume = balance.volume;
        }

        public ProjectileWeaponBalanceObject GetBalance()
        {
            ProjectileWeaponBalanceObject balance = new ProjectileWeaponBalanceObject();
            balance.name = name;
            
            if (_shell != null)
            {
                balance.speed = _shell.Speed;
                balance.radius = _shell.Radius;
            }
            
            balance.cooldown = Cooldown;
            balance.chargeTime = ChargeTime;

            return balance;
        }
    }
}