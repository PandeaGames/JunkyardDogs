using JunkyardDogs.Data;
using PandeaGames.Data.Static;

public class LootDataProvider : BundledStaticDataReferenceDirectory<
    AbstractLootData, 
    AbstractLootData, 
    LootStaticDataReference, 
    LootDataProvider>
{
    public const string FULL_PATH = "Assets/AssetBundles/Data/Loot/LootDataSource.asset";
        
    public LootDataProvider() : base("data", "LootDataSource")
    {
            
    }
}