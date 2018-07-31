using UnityEngine;
using System.Collections;
using JunkyardDogs.Simulation.Behavior;
using System;

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

            System.Action onInternalLoadSuccess = () =>
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

            if(ActionWeakReference != null)
            {
                objectsToLoad++;
                ActionWeakReference.LoadAsync<ScriptableObject>(onInternalAssetLoadSuccess, onLoadFailed);
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