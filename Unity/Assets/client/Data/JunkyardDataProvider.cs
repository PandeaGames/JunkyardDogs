using JunkyardDogs.Data;
using PandeaGames.Data.Static;

public class JunkyardDataProvider : BundledStaticDataReferenceDirectory<
    JunkyardData, 
    JunkyardData, 
    JunkyardStaticDataReference, 
    JunkyardDataProvider>
{
    public const string FULL_PATH = "Assets/AssetBundles/Data/Junkyard/Data/JunkyardDataSource.asset";
        
    public JunkyardDataProvider() : base("data", "JunkyardDataSource")
    {
            
    }
}