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

        public override void LoadAsync(Action onLoadSuccess, Action onLoadFailed)
        {
            if (Material != null)
            {
                SpecificationReference.LoadAsync<Specification>(
                    (asset, reference) =>
                    {
                        base.LoadAsync(onLoadSuccess, onLoadFailed);
                    },
                    onLoadFailed);
            }
            else
            {
                base.LoadAsync(onLoadSuccess, onLoadFailed);
            }
        }
    }
}
