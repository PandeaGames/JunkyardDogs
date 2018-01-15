using UnityEngine;
using System.Collections;
using JunkyardDogs.Specifications;

namespace JunkyardDogs.Components
{
    [CreateAssetMenu(fileName = "Motherboard", menuName = "Components/Motherboard", order = 3)]
    public class Motherboard : Component<Specifications.Motherboard>
    {
        [SerializeField]
        private CPU _cpu;

        public CPU CPU { get { return _cpu; } }
    }
}