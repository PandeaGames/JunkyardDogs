using PandeaGames.Data.Static;

namespace JunkyardDogs.Data
{
    public class TournamentDataProvider: BundledStaticDataReferenceDirectory<Tournament, Tournament, TournamentStaticDataReference, TournamentDataProvider>
    {
        public const string FULL_PATH = "Assets/AssetBundles/Data/Tournaments/TournamentDataSource.asset";
        
        public TournamentDataProvider() : base("data", "TournamentDataSource")
        {
            
        }
    }
}