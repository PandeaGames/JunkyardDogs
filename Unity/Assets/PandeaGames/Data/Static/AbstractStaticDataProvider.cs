using System;

namespace PandeaGames.Data.Static
{
    public abstract class AbstractStaticDataProvider<TStaticDataProvider> : IStaticDataProvider where TStaticDataProvider:AbstractStaticDataProvider<TStaticDataProvider>, new()
    {
        private static TStaticDataProvider _instance;
        public static TStaticDataProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Game.Instance.GetStaticDataPovider<TStaticDataProvider>();
                }
                
                return _instance;
            }
        }
        
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