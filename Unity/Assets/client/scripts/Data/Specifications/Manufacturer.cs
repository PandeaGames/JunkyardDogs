using UnityEngine;
using System.Collections;
using JunkyardDogs.Components;
using Component = JunkyardDogs.Components.Component;
using Data;

namespace JunkyardDogs.Specifications
{
    public class ManufacturerUtils
    {
        public static Component BuildComponent(WeakReference manufacturer, SpecificationCatalogue.Product product)
        {
            return BuildComponent(manufacturer, product.Specification, product.Material);
        }

        public static Component BuildComponent(WeakReference manufacturer, WeakReference spec, WeakReference material)
        {
            Component component = ComponentUtils.GenerateComponent(spec);
            PhysicalComponent physicalComponent = component as PhysicalComponent;

            component.Manufacturer = manufacturer;

            if (physicalComponent != null)
            {
                physicalComponent.Material = material;
            }

            return component;
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