﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JunkyardDogs.Simulation.Knowledge;
using System;

namespace JunkyardDogs.Simulation.Agent
{
    public class AgentState : ILoadableObject
    {
        public Data.WeakReference StateWeakReference { get; set; }
        public List<Directive> Directives { get; set; }
        public List<StateTransition> Transitions { set; get; }

        public Knowledge.State State
        {
            get { return StateWeakReference.Asset as Knowledge.State; }
        }

        public void LoadAsync(Action onLoadSuccess, Action onLoadFailed)
        {
            int objectsToLoad = 0;
            bool hasError = false;

            Action<ScriptableObject, Data.WeakReference> onInternalAssetLoadSuccess = (so,  refernce) =>
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

            if(StateWeakReference != null)
            {
                objectsToLoad++;
                StateWeakReference.LoadAsync<ScriptableObject>(onInternalAssetLoadSuccess, onLoadFailed);
            }

            if(Directives != null)
            {
                Directives.ForEach((directive) =>
                {
                    if (directive != null)
                    {
                        objectsToLoad++;
                        directive.LoadAsync(onInternalLoadSuccess, onInternalLoadError);
                    }
                });
            }

            if (Transitions != null)
            {
                Transitions.ForEach((transitions) =>
                {
                    if (transitions != null)
                    {
                        objectsToLoad++;
                        transitions.LoadAsync(onInternalLoadSuccess, onInternalLoadError);
                    }
                });
            }

            if (objectsToLoad == 0)
            {
                TaskProvider.Instance.RunTask(NullObjectsLoaded(), () => { onLoadSuccess(); });
            }
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