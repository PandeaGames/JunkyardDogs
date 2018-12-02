using Data;
using JunkyardDogs.Data;
using PandeaGames;
using PandeaGames.Data.Static;

public class JunkyardStaticDataLoader : ILoadableObject
{
    private bool _isLoaded;

    public bool IsLoaded()
    {
        return _isLoaded;
    }
    
    private Loader _loader;
    
    public JunkyardStaticDataLoader()
    {
        _loader = new Loader();
        _loader.AppendProvider(Game.Instance.GetStaticDataPovider<PandeaGameDataProvider>());
        _loader.AppendProvider(Game.Instance.GetStaticDataPovider<ViewStaticDataProvider>());
        _loader.AppendProvider(Game.Instance.GetStaticDataPovider<GameStaticDataProvider>());
    }

    public void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadError)
    {
        _loader.LoadAsync(() => { _isLoaded = true;
            onLoadSuccess();
        }, onLoadError);
    }
}