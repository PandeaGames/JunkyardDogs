using Data;
using PandeaGames;
using PandeaGames.Data.Static;

public class JunkyardStaticDataLoader : ILoadableObject
{
    private bool _isLoader;

    public bool IsLoaded()
    {
        return _isLoader;
    }
    private Loader _loader;
    
    public JunkyardStaticDataLoader()
    {
        _loader = new Loader();
        _loader.AppendProvider(Game.Instance.GetStaticDataPovider<PandeaGameDataProvider>());
    }

    public void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadError)
    {
        _loader.LoadAsync(onLoadSuccess, onLoadError);
    }
}