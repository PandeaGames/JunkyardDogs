using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Bot
{
    public class Mortar : Weapon
    {
        private MortarShell _shell;

        public MortarShell Shell { get { return _shell; } }
    }
}