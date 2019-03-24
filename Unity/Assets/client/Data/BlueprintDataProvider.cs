using PandeaGames.Data.Static;
using UnityEngine;

namespace JunkyardDogs.Data
{
    public class BlueprintDataProvider : BundledStaticDataReferenceDirectory<BlueprintDataBase, BlueprintDataBase, BlueprintStaticDataReference, BlueprintDataProvider>
    {
        public const string FULL_PATH = "Assets/AssetBundles/Data/Competitors/CompetitorBlueprintDataSource.asset";
        
        public BlueprintDataProvider() : base("data", "CompetitorBlueprintDataSource")
        {
            
        }
    }
    
    public class CompetitorBlueprintDataProvider : ChildStaticDataReferenceDirectory<BlueprintDataBase, CompetitorBlueprintData, BlueprintStaticDataReference, CompetitorBlueprintStaticDataReference, BlueprintDataProvider, CompetitorBlueprintDataProvider>
    {
    }
    
    public class ChassisBlueprintDataProvider : ChildStaticDataReferenceDirectory<BlueprintDataBase, ChassisBlueprintData, BlueprintStaticDataReference, ChassisBlueprintStaticDataReference, BlueprintDataProvider, ChassisBlueprintDataProvider>
    {
    }
    
    public class MotherboardBlueprintDataProvider : ChildStaticDataReferenceDirectory<BlueprintDataBase, MotherboardBlueprintData, BlueprintStaticDataReference, MotherboardBlueprintStaticDataReference, BlueprintDataProvider, MotherboardBlueprintDataProvider>
    {
    }
    
    public class PlateBlueprintDataProvider : ChildStaticDataReferenceDirectory<BlueprintDataBase, PlateBlueprintData, BlueprintStaticDataReference, PlateBlueprintStaticDataReference, BlueprintDataProvider, PlateBlueprintDataProvider>
    {
    }
    
    public class WeaponBlueprintDataProvider : ChildStaticDataReferenceDirectory<BlueprintDataBase, WeaponBlueprintData, BlueprintStaticDataReference, WeaponBlueprintStaticDataReference, BlueprintDataProvider, WeaponBlueprintDataProvider>
    {
    }
    
    public class AgentBlueprintDataProvider : ChildStaticDataReferenceDirectory<BlueprintDataBase, AgentBlueprintData, BlueprintStaticDataReference, AgentBlueprintStaticDataReference, BlueprintDataProvider, AgentBlueprintDataProvider>
    {
    }
    
    public class BotBlueprintDataProvider : ChildStaticDataReferenceDirectory<BlueprintDataBase, BotBlueprintData, BlueprintStaticDataReference, BotBlueprintStaticDataReference, BlueprintDataProvider, BotBlueprintDataProvider>
    {
    }
    
    public class WeaponProcessorBlueprintDataProvider : ChildStaticDataReferenceDirectory<BlueprintDataBase, WeaponProcessorBlueprintData, BlueprintStaticDataReference, WeaponProcessorBlueprintStaticDataReference, BlueprintDataProvider, WeaponProcessorBlueprintDataProvider>
    {
    }
}