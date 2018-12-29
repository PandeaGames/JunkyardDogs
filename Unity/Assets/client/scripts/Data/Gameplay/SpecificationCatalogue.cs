using UnityEngine;
using System.Collections;
using System;
using JunkyardDogs.Specifications;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;
using System.Threading.Tasks;
using Material = JunkyardDogs.Specifications.Material;

[Serializable]
[CreateAssetMenu(fileName = "SpecificationCatalogue", menuName = "GamePlay/SpecificationCatalogue", order = 3)]
public class SpecificationCatalogue : ScriptableObject
{
    [Serializable]
    public class Product : ILoadableObject
    {
        [SerializeField][WeakReference(typeof(Specification))]
        private WeakReference _specification;

        [SerializeField][WeakReference(typeof(JunkyardDogs.Specifications.Material))]
        private WeakReference _material;

        public WeakReference Specification { get { return _specification; } }
        public WeakReference Material { get { return _material; } }

        private bool _isLoaded;

        public bool IsLoaded()
        {
            throw new NotImplementedException();
        }

        public void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadFailed)
        {
            if (_specification == null)
            {
                Task task = Task.Run(() => onLoadFailed(new LoadException("No specification exists")));
            }
            else
            {
                _specification.LoadAssetAsync<Specification>(
                    (asset, reference) =>
                    {
                        _material.LoadAssetAsync<Material>(
                        (materialAsset, materialReference) =>
                        {
                            onLoadSuccess();
                            _isLoaded = true;
                        },
                        (e) => onLoadFailed(new LoadException(e.Message, e)));
                    },
                    (e) => onLoadFailed(new LoadException(e.Message, e)));
            }
        }
    }

    [SerializeField][WeakReference(typeof(Manufacturer))]
    private WeakReference _manufacturer;

    public WeakReference Manufacturer
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
