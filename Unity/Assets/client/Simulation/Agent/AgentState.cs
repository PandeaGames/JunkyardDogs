using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JunkyardDogs.Simulation.Knowledge;
using System;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;
using Data;
using JunkyardDogs.Data;

namespace JunkyardDogs.Simulation.Agent
{
    public class AgentState : ILoadableObject
    {
        public bool IsLoaded { get; private set; }
        public StateKnowledgeStaticDataReference StateWeakReference { get; set; }
        public List<Directive> Directives { get; set; }
        public List<StateTransition> Transitions { set; get; }

        public Knowledge.State State
        {
            get { return StateWeakReference.Data; }
        }

        public AgentState()
        {
            StateWeakReference = new StateKnowledgeStaticDataReference();
            Directives = new List<Directive>();
            Transitions = new List<StateTransition>();
        }

        public void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadFailed)
        {
            Loader loader = new Loader();
            loader.AppendProvider(Directives);
            loader.AppendProvider(Transitions);
            loader.LoadAsync(onLoadSuccess, onLoadFailed);
        }
    }
}