using JunkyardDogs.Behavior;
using PandeaGames.Data.Static;
using JunkyardDogs.Simulation.Behavior;

namespace JunkyardDogs.Data
{   
    public class ActionStaticDataProvider : BundledStaticDataReferenceDirectory<BehaviorAction, BehaviorAction, ActionStaticDataReference, ActionStaticDataProvider>
    {
        public const string FULL_PATH = "Assets/AssetBundles/Data/Simulation/ActionDataSource.asset";
        
        public ActionStaticDataProvider() : base("data", "ActionDataSource")
        {
            
        }
    }
}