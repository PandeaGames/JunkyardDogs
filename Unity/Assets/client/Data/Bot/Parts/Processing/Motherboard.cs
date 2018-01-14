using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Bot
{
    public class Motherboard : CircuitBoard
    {
        [SerializeField]
        private Quirk[] _quirks;

        [SerializeField]
        private CPU _cpu;

        public CPU CPU { get { return _cpu; } }
    }
}