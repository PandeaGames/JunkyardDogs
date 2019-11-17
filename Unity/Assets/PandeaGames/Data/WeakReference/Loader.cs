using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Data
{
    public class AnonymousProvider : ILoadableObject
    {
        public bool IsLoaded { get; private set; }

        private Action<LoadSuccess, LoadError> _action;
        
        public AnonymousProvider(Action<LoadSuccess, LoadError> action)
        {
            _action = action;
        }

        public void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadError)
        {
            _action.Invoke(() =>
            {
                IsLoaded = true;
                onLoadSuccess();
            }, onLoadError);
        }
    }
    
    public class LoaderGroup : ILoadableObject
    {
        public bool IsLoaded { get; private set; }
        
        protected List<ILoadableObject> _loadables;

        public LoaderGroup()
        {
            _loadables = new List<ILoadableObject>();
        }
        
        public void AppendProvider(ILoadableObject dataProvider)
        {
            if(dataProvider != null)
                _loadables.Add(dataProvider);
        }
        
        public void AppendProvider(IEnumerable<ILoadableObject> loadableList)
        {
            if (loadableList == null)
                return;
            
            foreach (var provider in loadableList)
            {
                AppendProvider(provider);
            }
        }

        public virtual void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadError)
        {
            int objectsToLoad = _loadables.Count;
            int objectsLoaded = 0;
            bool hasError = false;

            LoadSuccess onComplete = () =>
            {
                if (++objectsLoaded >= objectsToLoad)
                {
                    onLoadSuccess();
                }
            };
            LoadError onError = (e) =>
            {
                if (!hasError)
                {
                    onLoadError(e);
                }
                
                hasError = true;
            };
            
            if (_loadables.Count == 0)
            {
                TaskProvider.Instance.DelayedAction(() => onComplete());
            }
            else
            {
                foreach (ILoadableObject loadable in _loadables)
                {
                    loadable.LoadAsync(onComplete, onError);
                }
            }
        }
    }
    
    public class Loader : ILoadableObject
    {
        public bool IsLoaded { get; private set; }

        private List<LoaderGroup> _groups;

        public Loader()
        {
            _groups = new List<LoaderGroup>();
            AppendLoadGroup();
        }
        
        public Loader AppendLoadGroup()
        {
            LoaderGroup group = new LoaderGroup();
            _groups.Add(group);
            return this;
        }

        public Loader AppendProvider(IEnumerable<ILoadableObject> loadableList)
        {
            if (_groups.Count > 0)
            {
                _groups[_groups.Count - 1].AppendProvider(loadableList);
            }

            return this;
        }

        public Loader AppendProvider(Action<LoadSuccess, LoadError> action)
        {
            AppendProvider(new AnonymousProvider(action));
            return this;
        }

        public Loader AppendProvider(ILoadableObject dataProvider)
        {
            if (_groups.Count > 0 && dataProvider != null)
            {
                _groups[_groups.Count - 1].AppendProvider(dataProvider);
            }

            return this;
        }

        private void GroupLoaded(List<LoaderGroup>.Enumerator groups, LoadSuccess onLoadSuccess, LoadError onLoadError)
        {
            if (groups.MoveNext())
            {
                groups.Current.LoadAsync(() => { GroupLoaded(groups, onLoadSuccess, onLoadError); }, onLoadError); 
            }
            else
            {
                groups.Dispose();
                TaskProvider.Instance.DelayedAction(() => onLoadSuccess());
            }
        }

        public void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadError)
        {
            List<LoaderGroup>.Enumerator groups = _groups.GetEnumerator();
            if (groups.MoveNext())
            {
                groups.Current.LoadAsync(() => { GroupLoaded(groups, onLoadSuccess, onLoadError); }, onLoadError);
            }
            else
            {
                TaskProvider.Instance.DelayedAction(() => onLoadSuccess());
            }
        }
    }
}