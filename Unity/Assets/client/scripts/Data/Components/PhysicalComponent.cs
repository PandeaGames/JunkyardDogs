using UnityEngine;
using System.Collections;
using JunkyardDogs.Specifications;
using System;
using JunkyardDogs.Data;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

namespace JunkyardDogs.Components
{
    public interface IPhysicalComponent : IComponent
    {
        MaterialStaticDataReference Material { get; set; }
    }
    
    [Serializable]
    public class PhysicalComponent<TSpecification> : Component<TSpecification>, IPhysicalComponent where TSpecification:PhysicalSpecification
    {
        [SerializeField, MaterialStaticDataReference]
        private MaterialStaticDataReference _material = new MaterialStaticDataReference();
        
        public MaterialStaticDataReference Material
        {
            get => _material;
            set => _material = value;
        }
    }
}