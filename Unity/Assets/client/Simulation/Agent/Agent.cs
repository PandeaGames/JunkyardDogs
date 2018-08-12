using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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

        public void LoadAsync(Action onLoadSuccess, Action onLoadFailed)
        {
            int objectsToLoad = 0;
            bool hasError = false;

            Action onInternalLoadSuccess = () =>
            {
                if (--objectsToLoad <= 0)
                {
                    if (hasError)
                    {
                        onLoadFailed();
                    }
                    else
                    {
                        onLoadSuccess();
                    }
                }
            };

            Action onInternalLoadError = () =>
            {
                hasError = true;

                if (--objectsToLoad <= 0)
                {
                    onLoadFailed();
                }
            };

            if(States != null)
            {
                States.ForEach((state) =>
                {
                    if (state != null)
                    {
                        objectsToLoad++;
                        state.LoadAsync(onInternalLoadSuccess, onInternalLoadError);
                    }
                });
            }

            if(objectsToLoad == 0)
            {
                onLoadSuccess();
            }
        }

        public bool IsLoaded()
        {
            throw new NotImplementedException();
        }
    }
}