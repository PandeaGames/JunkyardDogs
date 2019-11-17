using UnityEngine;
using System.Collections;
using JunkyardDogs.Specifications;
using System;
using Polenter.Serialization;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;
using System.Threading.Tasks;
using JunkyardDogs.Data;

namespace JunkyardDogs.Components
{
    public static class ComponentUtils
    {
        public static IComponent GenerateComponent(SpecificationStaticDataReference spec, ManufacturerStaticDataReference manufacturer = null, MaterialStaticDataReference material = null)
        {
            JunkyardDogs.Components.IComponent component = null;

            Specification specData = spec.Data;

            if (specData == null)
            {
                return null;
                //throw new NullReferenceException("Specification data is null");
            }
            
            if(specData is JunkyardDogs.Specifications.Weapon)
            {
                component = new JunkyardDogs.Components.Weapon();
            }
            else if (specData is JunkyardDogs.Specifications.Chassis)
            {
                component = new JunkyardDogs.Components.Chassis();
            }
            else if (specData is JunkyardDogs.Specifications.WeaponChip)
            {
                component = new JunkyardDogs.Components.WeaponProcessor();
            }
            else if (specData is JunkyardDogs.Specifications.Plate)
            {
                component = new JunkyardDogs.Components.Plate();
            }
            else if (specData is JunkyardDogs.Specifications.CPU)
            {
                component = new JunkyardDogs.Components.CPU();
            }
            else if (specData is JunkyardDogs.Specifications.CircuitBoard)
            {
                component = new JunkyardDogs.Components.CircuitBoard();
            }
            else if (specData is Specifications.Motherboard)
            {
                component = new Motherboard();
            }
            else if (specData is JunkyardDogs.Specifications.Directive)
            {
                component = new JunkyardDogs.Components.Directive();
            }
            else if (specData is JunkyardDogs.Specifications.Engine)
            {
                component = new JunkyardDogs.Components.Engine();
            }

            if (component == null)
            {
                throw new NotSupportedException(string.Format("Specification type [{0}] could not be made into a component. It is not supported.", specData.GetType()));
            }

            component.Manufacturer = manufacturer;
            component.SpecificationReference = spec;

            if (component is IPhysicalComponent)
            {
                IPhysicalComponent physicalComponent = (IPhysicalComponent) component;

                if (physicalComponent != null)
                {
                    physicalComponent.Material = material;
                }
            }
            
            return component;
        }
    }
}