using System;

namespace PandeaGames.Data.Static
{
    public abstract class AbstractStaticDataProvider : IStaticDataProvider
    {
        private bool _isLoaded;

        public bool IsLoaded()
        {
            return _isLoaded;
        }

        protected abstract void InternalLoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadFailed);

        public void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadFailed)
        {
            InternalLoadAsync(() => { _isLoaded = true;
                onLoadSuccess();
            }, onLoadFailed);
        }
    }
}