using PandeaGames.Data.Static;

namespace PandeaGames
{
    public abstract class AsynchronousStaticDataProvider : IStaticDataProvider
    {
        public abstract void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadFailed);

        public bool IsLoaded { get; protected set; }
    }
}