﻿using UnityEngine;
using System.Collections;
using JunkyardDogs.Specifications;
using System;
using Polenter.Serialization;
using WeakReference = Data.WeakReference;
using System.Threading.Tasks;

namespace JunkyardDogs.Components
{
    [Serializable]
    public class Component : ILoadableObject
    {
        private bool _isLoaded;
        private bool _isLoading;

        public WeakReference SpecificationReference { get; set; }

        public WeakReference Manufacturer { get; set; }

        [ExcludeFromSerialization]
        public Specification Specification
        {
            get
            {
                return SpecificationReference.Asset as Specification;
            }
        }

        [SerializeField]
        public Distinction[] _distinctions { get; set; }

        public Component()
        {
            SpecificationReference = new WeakReference();
        }

        public virtual void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadFailed)
        {
            if (SpecificationReference == null)
            {
                Task task = Task.Run(() => { onLoadFailed(new LoadException("Specification refernence is 'null'")); });
            }
            else
            {
                SpecificationReference.LoadAssetAsync<Specification>(
                    (asset, reference) => 
                    {
                        onLoadSuccess();
                        _isLoaded = true;
                    },
                    (e) => onLoadFailed(new LoadException("Failed to load weak reference", e)));
            }
        }

        public bool IsLoaded()
        {
            return _isLoaded;
        }

        public virtual void Dismantle(Inventory inventory)
        {
            inventory.AddComponent(this);
        }
    }
}