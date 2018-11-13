using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Data {
    public class WeakReferenceList<T> : ScriptableObject,IEnumerable<WeakReference>, ILoadableObject where T:UnityEngine.Object
    {
        [SerializeField][WeakReference]
        private List<WeakReference> _list;

        private bool _isLoaded;
    
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
                            _isLoaded = true;
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
    
        public bool IsLoaded()
        {
            return _isLoaded;
        }
    }

}
