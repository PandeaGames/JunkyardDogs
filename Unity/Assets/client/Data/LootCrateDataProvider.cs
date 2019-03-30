using JunkyardDogs.Data;
using PandeaGames.Data.Static;

public class LootCrateDataProvider : BundledStaticDataReferenceDirectory<
    AbstractLootCrateData, 
    AbstractLootCrateData, 
    LootCrateStaticDataReference, 
    LootCrateDataProvider>
{
    public const string FULL_PATH = "Assets/AssetBundles/Data/Loot/LootCrateDataSource.asset";
        
    public LootCrateDataProvider() : base("data", "LootCrateDataSource")
    {
            
    }
}