using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JunkyardDogs.Simulation.Knowledge;
using System;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;
using Data;

namespace JunkyardDogs.Simulation.Agent
{
    public class AgentState : ILoadableObject
    {
        public WeakReference StateWeakReference { get; set; }
        public List<Directive> Directives { get; set; }
        public List<StateTransition> Transitions { set; get; }

        public Knowledge.State State
        {
            get { return StateWeakReference.Asset as Knowledge.State; }
        }

        public AgentState()
        {
            StateWeakReference = new WeakReference();
            Directives = new List<Directive>();
            Transitions = new List<StateTransition>();
        }

        public void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadFailed)
        {
            Loader loader = new Loader();
            loader.AppendProvider(StateWeakReference);
            loader.AppendProvider(Directives);
            loader.AppendProvider(Transitions);
            loader.LoadAsync(onLoadSuccess, onLoadFailed);
        }

        private IEnumerator NullObjectsLoaded()
        {
            yield return 0;
        }

        public bool IsLoaded()
        {
            throw new NotImplementedException();
        }
    }
}