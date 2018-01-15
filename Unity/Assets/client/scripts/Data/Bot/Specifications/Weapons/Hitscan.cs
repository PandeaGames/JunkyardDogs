using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Specifications
{
    [CreateAssetMenu(fileName = "Hitscan", menuName = "Specifications/Hitscan", order = 6)]
    public class Hitscan : Weapon
    {
        private HitscanBullet _shell;

        public HitscanBullet Shell { get { return _shell; } }
    }
}
