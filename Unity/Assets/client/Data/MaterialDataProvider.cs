using JunkyardDogs.Specifications;
using PandeaGames.Data.Static;

namespace JunkyardDogs.Data
{
    
    public class MaterialDataProvider : BundledStaticDataReferenceDirectory<Material, MaterialStaticDataReference, MaterialDataProvider>
    {
        public const string FULL_PATH = "Assets/AssetBundles/Data/Products/Materials/MaterialDataSource.asset";
        
        public MaterialDataProvider() : base("data", "MaterialDataSource")
        {
            
        }
    }
}