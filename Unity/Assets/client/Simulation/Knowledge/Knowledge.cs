using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Simulation.Knowledge
{
    public abstract class Knowledge : AbstractStaticData
    {
        public abstract bool IsTrue(Information.Information information);
    }
}