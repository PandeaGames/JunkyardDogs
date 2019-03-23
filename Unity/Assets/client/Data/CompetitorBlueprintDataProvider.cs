using PandeaGames.Data.Static;
using UnityEngine;

namespace JunkyardDogs.Data
{
    public class CompetitorBlueprintDataProvider : BundledStaticDataReferenceDirectory<CompetitorBlueprintData, CompetitorBlueprintData, CompetitorBlueprintStaticDataReference, CompetitorBlueprintDataProvider>
    {
        public const string FULL_PATH = "Assets/AssetBundles/Data/Competitors/CompetitorBlueprintDataSource.asset";
        
        public CompetitorBlueprintDataProvider() : base("data", "CompetitorBlueprintDataSource")
        {
            
        }
    }
}