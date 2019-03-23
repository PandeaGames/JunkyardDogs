using JunkyardDogs.Specifications;
using PandeaGames.Data.Static;
using UnityEngine;

namespace JunkyardDogs.Data
{
    [CreateAssetMenu]
    public class ManufacturerDataProvider : BundledStaticDataReferenceDirectory<Manufacturer, Manufacturer, ManufacturerStaticDataReference, ManufacturerDataProvider>
    {
        public const string FULL_PATH = "Assets/AssetBundles/Data/Products/ManufactuererDataSource.asset";
        
        public ManufacturerDataProvider() : base("data", "ManufactuererDataSource")
        {
            
        }
    }
}