using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Specifications
{
    [CreateAssetMenu(fileName = "ProjectileWeapon", menuName = "Specifications/ProjectileWeapon", order = 6)]
    public class ProjectileWeapon : Weapon
    {
        private Bullet _shell;
        public Bullet Shell { get { return _shell; } }
    }
}