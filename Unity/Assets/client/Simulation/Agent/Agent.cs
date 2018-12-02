using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Data;

namespace JunkyardDogs.Simulation.Agent
{
    public class Agent : ILoadableObject
    {
        public List<AgentState> States { get; set; }
        private int DefaultState { get; set; }

        public Agent()
        {
            States = new List<AgentState>();
        }
        
        public AgentState InitialState
        {
            get { return States[DefaultState]; }
        }

        public void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadFailed)
        {
            Loader loader = new Loader();
            
            States.ForEach((state) =>
            {
                loader.AppendProvider(state);
            });
            
            loader.LoadAsync(onLoadSuccess, onLoadFailed);
        }

        public bool IsLoaded()
        {
            throw new NotImplementedException();
        }
    }
}