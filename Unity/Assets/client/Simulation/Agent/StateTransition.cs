using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using JunkyardDogs.Simulation.Knowledge;
using System;
using Data;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

namespace JunkyardDogs.Simulation.Agent
{
    public class StateTransition : ILoadableObject
    {
        private bool _isLoaded;
        public List<WeakReference> CriteriaReferences { get; set; }
        public AgentState StateToTransition { get; set; }
        public bool IsLoaded { get; private set; }

        public List<Knowledge.Knowledge> Criteria
        {
            get
            {
                List<Knowledge.Knowledge> criteria = new List<Knowledge.Knowledge>();
                CriteriaReferences.ForEach((reference) => criteria.Add(reference.Asset as Knowledge.Knowledge));
                return criteria;
            }
        }

        public void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadFailed)
        {
            Loader loader = new Loader();
            loader.AppendProvider(CriteriaReferences);
            loader.AppendProvider(StateToTransition);
            loader.LoadAsync(onLoadSuccess, onLoadFailed);
        }
        private IEnumerator NullObjectsLoaded()
        {
            yield return 0;
        }
    }
}