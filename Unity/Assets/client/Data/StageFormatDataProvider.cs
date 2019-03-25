using PandeaGames.Data.Static;

namespace JunkyardDogs.Data
{
    public class StageFormatDataProvider: BundledStaticDataReferenceDirectory< StageFormat,  StageFormat,  StageFormatStaticDataReference, StageFormatDataProvider>
    {
        public const string FULL_PATH = "Assets/AssetBundles/Data/Tournaments/StageFormatDataSource.asset";
        
        public StageFormatDataProvider() : base("data", "StageFormatDataSource")
        {
            
        }
    }
}