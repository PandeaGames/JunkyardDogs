using PandeaGames.Data.Static;

namespace JunkyardDogs.Data
{
    public class NationalityDataProvider : BundledStaticDataReferenceDirectory<Nationality, Nationality, NationalityStaticDataReference, NationalityDataProvider>
    {
        public const string FULL_PATH = "Assets/AssetBundles/Data/Nations/NationalityDataSource.asset";
        
        public NationalityDataProvider() : base("data", "NationalityDataSource")
        {
            
        }
    }
}