using UnityEngine;
using System.Collections;
using JunkyardDogs.Specifications;

namespace JunkyardDogs.Components
{
    [CreateAssetMenu(fileName = "CPU", menuName = "Components/CPU", order = 3)]
    public class CPU : Component<Specifications.CPU>
    {
        [SerializeField]
        private Quirk[] _quirks;
    }
}