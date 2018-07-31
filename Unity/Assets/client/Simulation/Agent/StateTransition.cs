using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using JunkyardDogs.Simulation.Knowledge;
using System;

namespace JunkyardDogs.Simulation.Agent
{
    public class StateTransition
    {
        public List<Data.WeakReference> CriteriaReferences { get; set; }
        public AgentState StateToTransition { get; set; }

        public List<Knowledge.Knowledge> Criteria
        {
            get
            {
                List<Knowledge.Knowledge> criteria = new List<Knowledge.Knowledge>();
                CriteriaReferences.ForEach((reference) => criteria.Add(reference.Asset as Knowledge.Knowledge));
                return criteria;
            }
        }

        public void LoadAsync(System.Action onLoadSuccess, System.Action onLoadFailed)
        {
            int objectsToLoad = 0;
            bool hasError = false;

            Action<ScriptableObject, Data.WeakReference> onInternalAssetLoadSuccess = (so, refernce) =>
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

            if (CriteriaReferences != null)
            {
                CriteriaReferences.ForEach((reference) =>
                {
                    if (reference != null)
                    {
                        objectsToLoad++;
                        reference.LoadAsync(onInternalAssetLoadSuccess, onInternalLoadError);
                    }
                });
            }

            if(StateToTransition != null)
            {
                objectsToLoad++;
                StateToTransition.LoadAsync(onInternalLoadSuccess, onInternalLoadError);
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
    }
}