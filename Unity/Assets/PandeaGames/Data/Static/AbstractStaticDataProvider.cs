using System;

namespace PandeaGames.Data.Static
{
    public abstract class AbstractStaticDataProvider : IStaticDataProvider
    {
        public bool IsLoaded { get; private set; }

        protected abstract void InternalLoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadFailed);

        public void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadFailed)
        {
            InternalLoadAsync(() => { IsLoaded = true;
                onLoadSuccess();
            }, onLoadFailed);
        }
    }
}