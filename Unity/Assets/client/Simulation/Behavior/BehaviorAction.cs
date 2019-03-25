using System;
using UnityEngine;
using System.Collections;
using JunkyardDogs.Simulation;


public class BehaviorAction : ScriptableObject
{
    public virtual ActionResult GetResult()
    {
        throw new NotImplementedException();
    }
}
