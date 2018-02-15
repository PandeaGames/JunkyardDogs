using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Simulation.Behavior
{
    public abstract class Action : ScriptableObject
    {
        public abstract ActionResult GetResult();
    }
}