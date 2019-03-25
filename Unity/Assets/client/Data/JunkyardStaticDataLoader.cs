using Data;
using JunkyardDogs.Data;
using PandeaGames;
using PandeaGames.Data.Static;

public class JunkyardStaticDataLoader : ILoadableObject
{
    public bool IsLoaded { get; private set; }
    
    private Loader _loader;
    
    public JunkyardStaticDataLoader()
    {
        _loader = new Loader();
        _loader.AppendProvider(Game.Instance.GetStaticDataPovider<PandeaGameDataProvider>());
        _loader.AppendProvider(Game.Instance.GetStaticDataPovider<ViewStaticDataProvider>());
        _loader.AppendProvider(Game.Instance.GetStaticDataPovider<GameStaticDataProvider>());
        _loader.AppendProvider(Game.Instance.GetStaticDataPovider<NationalityDataProvider>());
        _loader.AppendProvider(Game.Instance.GetStaticDataPovider<SpecificationDataProvider>());
        _loader.AppendProvider(Game.Instance.GetStaticDataPovider<ParticipantDataProvider>());
        _loader.AppendProvider(Game.Instance.GetStaticDataPovider<BlueprintDataProvider>());
        _loader.AppendProvider(Game.Instance.GetStaticDataPovider<MaterialDataProvider>());
        _loader.AppendProvider(Game.Instance.GetStaticDataPovider<ActionStaticDataProvider>());
        _loader.AppendProvider(Game.Instance.GetStaticDataPovider<StateKnowledgeStaticDataProvider>());
        _loader.AppendProvider(Game.Instance.GetStaticDataPovider<ManufacturerDataProvider>());
        _loader.AppendProvider(Game.Instance.GetStaticDataPovider<TournamentDataProvider>());
        _loader.AppendProvider(Game.Instance.GetStaticDataPovider<TournamentFormatDataProvider>());
        _loader.AppendProvider(Game.Instance.GetStaticDataPovider<StageFormatDataProvider>());
    }

    public void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadError)
    {
        _loader.LoadAsync(() => { IsLoaded = true;
            onLoadSuccess();
            IsLoaded = true;
        }, onLoadError);
    }
}