using System;
using UnityEngine;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;
using PandeaGames.Data.WeakReferences;

namespace PandeaGames.Data
{
    public abstract class LoadableConfigurationData : ScriptableObject, ILoadableObject
    {
        private bool _isLoaded;

        protected abstract WeakReference[] WeakReferences();

        protected virtual void AdjunctLoad(LoadSuccess onComplete, LoadError onError)
        {
            onComplete();
        }
        
        public bool IsLoaded()
        {
            return _isLoaded;
        }

        public void LoadAsync(LoadSuccess onComplete, LoadError onError)
        {
            WeakReference[] weakReferences = WeakReferences();
            int assetCount = weakReferences.Length;
            int totalLoadsRequired = assetCount + 1;
            int loadsCompleted = 0;
            bool hasFailed = false;
            
            Action<WeakReference> assetLoaded = (reference) =>
            {
                if (++loadsCompleted >= totalLoadsRequired)
                {
                    onComplete();
                }
            };
            
            
            LoadSuccess adjunctLoaded = () =>
            {
                if (++loadsCompleted >= totalLoadsRequired)
                {
                    onComplete();
                }
            };
            
            Action<WeakReferenceException> assetLoadFailed = (e) =>
            {
                if (!hasFailed)
                {
                    onError(new LoadException("Failed to load references", e));
                }
                
                hasFailed = true;
            };
            
            
            foreach (WeakReference reference in weakReferences)
            {
                reference.LoadAssetAsync(assetLoaded, assetLoadFailed);
            }

            AdjunctLoad(adjunctLoaded, onError);
        }
    }
}