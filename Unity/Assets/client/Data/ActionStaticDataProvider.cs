using JunkyardDogs.Behavior;
using PandeaGames.Data.Static;

namespace JunkyardDogs.Data
{   
    public class ActionStaticDataProvider : BundledStaticDataReferenceDirectory<Action, Action, ActionStaticDataReference, ActionStaticDataProvider>
    {
        public const string FULL_PATH = "Assets/AssetBundles/Data/Simulation/ActionDataSource.asset";
        
        public ActionStaticDataProvider() : base("data", "ActionDataSource")
        {
            
        }
    }
}