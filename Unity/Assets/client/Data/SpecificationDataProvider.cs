using JunkyardDogs.Specifications;
using PandeaGames.Data.Static;

namespace JunkyardDogs.Data
{
    public class SpecificationDataProvider: BundledStaticDataReferenceDirectory<Specification, SpecificationStaticDataReference, SpecificationDataProvider>
    {
        public const string FULL_PATH = "Assets/AssetBundles/Data/Products/SpecificationDataSource.asset";
        
        public SpecificationDataProvider() : base("data", "SpecificationDataSource")
        {
            
        }
    }
}