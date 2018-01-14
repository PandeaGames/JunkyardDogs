using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Bot
{
    public class Hitscan : Weapon
    {
        private HitscanBullet _shell;

        public HitscanBullet Shell { get { return _shell; } }
    }
}
