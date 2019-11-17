using System;
using Data;

namespace PandeaGames.Data
{
    public class SynchronousLoaderGroup : LoaderGroup
    {
        public SynchronousLoaderGroup() : base()
        {
            
        }
        
        public override void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadError)
        {
            DoLoad(onLoadSuccess, onLoadError);
        }

        private void DoLoad(LoadSuccess onLoadSuccess, LoadError onLoadError)
        {
            DoLoad(onLoadSuccess, onLoadError, 0);
        }

        private void DoLoad(LoadSuccess onLoadSuccess, LoadError onLoadError, int i)
        {
            _loadables[i].LoadAsync(() =>
            {
                i++;
                
                if (i < _loadables.Count)
                {
                    DoLoad(onLoadSuccess, onLoadError, i);
                }
                else
                {
                    onLoadSuccess();
                }
                
            }, onLoadError);
        }
    }
}