using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Simulation.Knowledge
{
    public abstract class Knowledge : ScriptableObject
    {
        public abstract bool IsTrue(Information.Information information);
    }
}