using PandeaGames.Data.Static;
using JunkyardDogs.Data;

public class BreakpointDataProvider : BundledStaticDataReferenceDirectory<
    BreakpointData, 
    BreakpointData, 
    BreakpointStaticDataReference,
    BreakpointDataProvider>
{
    public const string FULL_PATH = "Assets/AssetBundles/Data/Breakpoints/BreakpointDataSource.asset";
        
    public BreakpointDataProvider() : base("data", "BreakpointDataSource")
    {
        
    }
}