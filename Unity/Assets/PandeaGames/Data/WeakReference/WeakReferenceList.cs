using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace PandeaGames.Data.WeakReferences {
    public class WeakReferenceList<T> : ScriptableObject,IEnumerable<WeakReference>, ILoadableObject where T:UnityEngine.Object
    {
        [SerializeField][WeakReference]
        private List<WeakReference> _list;

        public bool IsLoaded { get; private set; }
    
        public void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadFailed)
        {
            if (_list != null)
            {
                int loaded = 0;
                _list.ForEach((weakReference) =>
                {
                    weakReference.LoadAssetAsync<T>((data, reference) =>
                    {
                        if (++loaded >= _list.Count)
                        {
                            IsLoaded = true;
                            onLoadSuccess();
                        }
                    }, (exception) => { onLoadFailed(new LoadException("Failed to load asset in list.", exception)); });
                });
            }
        }

        public IEnumerator<WeakReference> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

}
