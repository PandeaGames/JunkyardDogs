using UnityEngine;
using System.Collections;
using JunkyardDogs.Simulation.Behavior;
using System;
using Data;

namespace JunkyardDogs.Simulation.Agent
{
    public class Directive: ILoadableObject
    {
        public Data.WeakReference ActionWeakReference { get; set; }

        public Behavior.Action Action
        {
            get { return ActionWeakReference.Asset as Behavior.Action; }
        }

        public bool IsLoaded()
        {
            throw new NotImplementedException();
        }

        public void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadFailed)
        {
            Loader loader = new Loader();
            loader.AppendProvider(ActionWeakReference);
            loader.LoadAsync(onLoadSuccess, onLoadFailed);
        }

        private IEnumerator NullObjectsLoaded()
        {
            yield return 0;
        }
    }
}