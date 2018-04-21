using UnityEngine;
using System.Collections;
using JunkyardDogs.Simulation;
using Weapon = JunkyardDogs.Specifications.Weapon;

namespace JunkyardDogs.Specifications
{
    [CreateAssetMenu(fileName = "ProjectileWeapon", menuName = "Specifications/ProjectileWeapon", order = 6)]
    public class ProjectileWeapon : Weapon
    {
        [SerializeField]
        private Bullet _shell;
        public Bullet Shell { get { return _shell; } }

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
    }
}