using UnityEngine;
using System.Collections;
using System;
using JunkyardDogs.Specifications;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;
using System.Threading.Tasks;
using JunkyardDogs.Data;
using Material = JunkyardDogs.Specifications.Material;

[Serializable]
[CreateAssetMenu(fileName = "SpecificationCatalogue", menuName = "GamePlay/SpecificationCatalogue", order = 3)]
public class SpecificationCatalogue : ScriptableObject
{
    [Serializable]
    public class Product
    {
        [SerializeField, StaticDataReference(path:SpecificationDataProvider.FULL_PATH)]
        private SpecificationStaticDataReference _specification;

        [SerializeField][StaticDataReference(path:MaterialDataProvider.FULL_PATH)]
        private MaterialStaticDataReference _material;

        public SpecificationStaticDataReference Specification { get { return _specification; } }
        public MaterialStaticDataReference Material { get { return _material; } }
    }

    [SerializeField][StaticDataReference(path:ManufacturerDataProvider.FULL_PATH)]
    private ManufacturerStaticDataReference _manufacturer;

    public ManufacturerStaticDataReference Manufacturer
    {
        get
        {
            return _manufacturer;
        }
    }

    [SerializeField]
    private Product[] _specifications;

    public Product[] Products
    {
        get
        {
            return _specifications;
        }
    }
}
