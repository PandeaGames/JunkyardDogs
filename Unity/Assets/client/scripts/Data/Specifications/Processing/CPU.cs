using UnityEngine;
using System.Collections;
using JunkyardDogs.Components;

namespace JunkyardDogs.Specifications
{
    [CreateAssetMenu(fileName = "CPU", menuName = "Specifications/CPU", order = 3)]
    public class CPU : Processor
    {
        [SerializeField]
        private Quirk[] _quirks;
    }
}