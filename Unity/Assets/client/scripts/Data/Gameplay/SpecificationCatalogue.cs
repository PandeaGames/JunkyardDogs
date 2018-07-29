using UnityEngine;
using System.Collections;
using System;
using JunkyardDogs.Specifications;
using WeakReference = Data.WeakReference;
using System.Threading.Tasks;

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

        public void LoadAsync(Action onLoadSuccess, Action onLoadFailed)
        {
            if (_specification == null)
            {
                Task task = Task.Run(onLoadFailed);
            }
            else
            {
                _specification.LoadAsync<Specification>(
                    (asset, reference) =>
                    {
                        _material.LoadAsync<Specification>(
                        (materialAsset, materialReference) =>
                        {
                            onLoadSuccess();
                            _isLoaded = true;
                        },
                        onLoadFailed);
                    },
                    onLoadFailed);
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
