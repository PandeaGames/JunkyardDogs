using System;
using UnityEngine;
using System.Collections;
using JunkyardDogs.Components;
using Component = JunkyardDogs.Components.Component;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

namespace JunkyardDogs.Specifications
{
    public class ManufacturerUtils
    {
        public static void BuildComponent(WeakReference manufacturer, SpecificationCatalogue.Product product, Action<Component> onComplete)
        {
            BuildComponent(manufacturer, product.Specification, product.Material, onComplete);
        }

        public static void BuildComponent(WeakReference manufacturer, WeakReference spec, WeakReference material, Action<Component> onComplete)
        {
            ComponentUtils.GenerateComponent(spec, (component) =>
            {
                PhysicalComponent physicalComponent = component as PhysicalComponent;

                component.Manufacturer = manufacturer;

                if (physicalComponent != null)
                {
                    physicalComponent.Material = material;
                }

                onComplete(component);
            }); 
        }
    }

    [CreateAssetMenu(fileName = "Manufacturer", menuName = "Specifications/Manufacturer", order = 2)]
    public class Manufacturer : ScriptableObject
    {
        [SerializeField]
        private Distinction _distinctions;

        [SerializeField]
        private Nationality _nationality;
    }
}