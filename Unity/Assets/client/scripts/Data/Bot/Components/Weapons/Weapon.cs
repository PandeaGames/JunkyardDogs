using UnityEngine;
using System.Collections;
using JunkyardDogs.Specifications;

namespace JunkyardDogs.Components
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Components/Weapon", order = 3)]
    public class Weapon : Component<Specifications.Weapon>
    {
    }
}