using JunkyardDogs.Specifications;
using PandeaGames.Data.Static;

namespace JunkyardDogs.Data
{
    public class SpecificationDataProvider: BundledStaticDataReferenceDirectory<Specification, Specification, SpecificationStaticDataReference, SpecificationDataProvider>
    {
        public const string FULL_PATH = "Assets/AssetBundles/Data/Products/SpecificationDataSource.asset";
        
        public SpecificationDataProvider() : base("data", "SpecificationDataSource")
        {
            
        }
    }
    
    public class WeaponDataProvider: BundledStaticDataReferenceDirectory<Specification, Weapon, WeaponStaticDataReference, WeaponDataProvider>
    {
        public const string FULL_PATH = "Assets/AssetBundles/Data/Products/SpecificationDataSource.asset";
        
        public WeaponDataProvider() : base("data", "SpecificationDataSource")
        {
            
        }
    }
}