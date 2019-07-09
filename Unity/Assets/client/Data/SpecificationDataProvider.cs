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
    
    public class WeaponDataProvider: ChildStaticDataReferenceDirectory<Specification, Weapon, SpecificationStaticDataReference, WeaponStaticDataReference, SpecificationDataProvider, WeaponDataProvider>
    {
    }
    
    public class CPUDataProvider: ChildStaticDataReferenceDirectory<Specification, CPU, SpecificationStaticDataReference, CPUStaticDataReference, SpecificationDataProvider, CPUDataProvider>
    {
    }
}