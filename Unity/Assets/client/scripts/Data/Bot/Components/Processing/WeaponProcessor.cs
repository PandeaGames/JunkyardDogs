using UnityEngine;
using System.Collections;
using JunkyardDogs.Specifications;

namespace JunkyardDogs.Components
{

    [CreateAssetMenu(fileName = "WeaponProcessor", menuName = "Components/WeaponProcessor", order = 3)]

    public class WeaponProcessor : Component<Specifications.WeaponChip>
    {
        public Components.Weapon _weapon;

        public Components.Weapon Weapon { get { return _weapon; } set { _weapon = value; } }
    }
}