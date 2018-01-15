using UnityEngine;
using System.Collections;
using JunkyardDogs.Specifications;

namespace JunkyardDogs.Components
{
    [CreateAssetMenu(fileName = "CircuitBoard", menuName = "Components/CircuitBoard", order = 3)]
    public class CircuitBoard : Component<Specifications.CircuitBoard>
    {
    }
}