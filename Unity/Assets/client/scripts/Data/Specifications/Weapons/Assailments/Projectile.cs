using UnityEngine;
using System.Collections;
using JunkyardDogs.Data.Balance;

namespace JunkyardDogs.Specifications
{
    [CreateAssetMenu(fileName = "Projectile", menuName = "Specifications/Projectile", order = 7)]
    public class Projectile : Assailer, IStaticDataBalance<ProjectileWeaponBalanceObject>
    {
        [SerializeField]
        private float _speed;
        [SerializeField]
        private float _radius;

        public float Speed { get { return _speed; } }
        public float Radius { get { return _radius; } }
        public void ApplyBalance(ProjectileWeaponBalanceObject balance)
        {
            _speed = balance.speed;
            _radius = balance.radius;
            Damage = balance.damage;
        }

        public ProjectileWeaponBalanceObject GetBalance()
        {
            throw new System.NotImplementedException();
        }
    }
}