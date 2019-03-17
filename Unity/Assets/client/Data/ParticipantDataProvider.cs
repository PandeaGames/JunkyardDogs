using PandeaGames.Data.Static;

namespace JunkyardDogs.Data
{
    
    public class ParticipantDataProvider : BundledStaticDataReferenceDirectory<ParticipantData,ParticipantStaticDataReference, ParticipantDataProvider>
    {
        public const string FULL_PATH = "Assets/AssetBundles/Data/Competitors/ParticipantDataSource.asset";
        
        public ParticipantDataProvider() : base("data", "ParticipantDataSource")
        {
            
        }
    }
}