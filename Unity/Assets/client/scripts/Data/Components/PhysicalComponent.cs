using UnityEngine;
using System.Collections;
using JunkyardDogs.Specifications;
using System;
using WeakReference = Data.WeakReference;

namespace JunkyardDogs.Components
{
    public class PhysicalComponent : Component
    {
        public WeakReference Material { get; set; }

        public override void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadFailed)
        {
            if (Material != null)
            {
                SpecificationReference.LoadAssetAsync<Specification>(
                    (asset, reference) =>
                    {
                        base.LoadAsync(onLoadSuccess, onLoadFailed);
                    },
                    (e) => onLoadFailed(new LoadException("Failed to load specification", e)));
            }
            else
            {
                base.LoadAsync(onLoadSuccess, onLoadFailed);
            }
        }
    }
}
