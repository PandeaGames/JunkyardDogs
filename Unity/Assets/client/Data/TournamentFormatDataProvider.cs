using PandeaGames.Data.Static;

namespace JunkyardDogs.Data
{
    public class TournamentFormatDataProvider: BundledStaticDataReferenceDirectory<TournamentFormat, TournamentFormat, TournamentFormatStaticDataReference, TournamentFormatDataProvider>
    {
        public const string FULL_PATH = "Assets/AssetBundles/Data/Tournaments/TournamentFormatDataSource.asset";
        
        public TournamentFormatDataProvider() : base("data", "TournamentFormatDataSource")
        {
            
        }
    }
}