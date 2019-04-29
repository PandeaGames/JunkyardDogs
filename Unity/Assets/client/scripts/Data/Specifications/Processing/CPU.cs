using UnityEngine;
using JunkyardDogs.Components;

namespace JunkyardDogs.Specifications
{
    [CreateAssetMenu(fileName = "CPU", menuName = "Specifications/CPU", order = 3)]
    public class CPU : Processor
    {
        [SerializeField]
        private Distinction[] _quirks;

        [SerializeField]
        public int Aggressiveness;
        
        [SerializeField]
        public int Evasiveness;
        
        [SerializeField]
        public int Cautiousness;

        [SerializeField]
        public int DirectiveSlotCount;
    }
}