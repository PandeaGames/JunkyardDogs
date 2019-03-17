using System;
using UnityEngine;
using System.Collections;
using JunkyardDogs.Components;
using JunkyardDogs.Data;
using Component = JunkyardDogs.Components.Component;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

namespace JunkyardDogs.Specifications
{
    public class ManufacturerUtils
    {
        public static Component BuildComponent(ManufacturerStaticDataReference manufacturer, SpecificationCatalogue.Product product)
        {
            return BuildComponent(manufacturer, product.Specification, product.Material);
        }

        public static Component BuildComponent(ManufacturerStaticDataReference manufacturer, SpecificationStaticDataReference spec, MaterialStaticDataReference material)
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