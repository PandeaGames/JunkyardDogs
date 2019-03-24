using UnityEngine;
using System.Collections;
using JunkyardDogs.Simulation.Behavior;
using System;
using Data;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

namespace JunkyardDogs.Simulation.Agent
{
    public class Directive: ILoadableObject
    {
        public WeakReference ActionWeakReference { get; set; }
        public bool IsLoaded { get; private set; }

        public Behavior.Action Action
        {
            get { return ActionWeakReference.Asset as Behavior.Action; }
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