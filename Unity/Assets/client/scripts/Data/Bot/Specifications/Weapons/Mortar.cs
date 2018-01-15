using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Specifications
{
    [CreateAssetMenu(fileName = "Mortar", menuName = "Specifications/Mortar", order = 6)]
    public class Mortar : Weapon
    {
        private MortarShell _shell;

        public MortarShell Shell { get { return _shell; } }
    }
}