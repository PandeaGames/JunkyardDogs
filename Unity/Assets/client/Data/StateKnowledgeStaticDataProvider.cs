using JunkyardDogs.Simulation.Knowledge;
using PandeaGames.Data.Static;
using UnityEngine;

namespace JunkyardDogs.Data
{
    
    public class StateKnowledgeStaticDataProvider : BundledStaticDataReferenceDirectory<State, State, StateKnowledgeStaticDataReference, StateKnowledgeStaticDataProvider>
    {
        public const string FULL_PATH = "Assets/AssetBundles/Data/Simulation/Knowledge/StateKnowledgeDataSource.asset";
        
        public StateKnowledgeStaticDataProvider() : base("data", "StateKnowledgeDataSource")
        {
            
        }
    }
}