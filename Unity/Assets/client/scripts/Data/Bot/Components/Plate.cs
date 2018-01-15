using UnityEngine;
using System.Collections;
using JunkyardDogs.Specifications;

namespace JunkyardDogs.Components
{
    [CreateAssetMenu(fileName = "Plate", menuName = "Components/Plate", order = 3)]
    public class Plate : Component<Specifications.Plate>
    {
    }
}