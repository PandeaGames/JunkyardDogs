using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Bot
{
    public class ProjectileWeapon : Weapon
    {
        private Bullet _shell;
        public Bullet Shell { get { return _shell; } }
    }
}