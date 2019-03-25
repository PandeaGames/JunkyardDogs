using UnityEngine;
using System.Collections;
using JunkyardDogs.Simulation.Behavior;
using System;
using Data;
using JunkyardDogs.Data;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

namespace JunkyardDogs.Simulation.Agent
{
    public class Directive
    {
        public ActionStaticDataReference ActionWeakReference { get; set; }

        public BehaviorAction BehaviorAction
        {
            get { return ActionWeakReference.Data; }
        }
    }
}