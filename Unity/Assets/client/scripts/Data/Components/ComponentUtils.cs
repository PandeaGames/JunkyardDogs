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
        public static Component GenerateComponent(SpecificationStaticDataReference spec)
        {
            JunkyardDogs.Components.Component component = null;

            if(spec.Data is JunkyardDogs.Specifications.Weapon)
            {
                component = new JunkyardDogs.Components.Weapon();
            }
            else if (spec.Data is JunkyardDogs.Specifications.Chassis)
            {
                component = new JunkyardDogs.Components.Chassis();
            }
            else if (spec.Data is JunkyardDogs.Specifications.WeaponChip)
            {
                component = new JunkyardDogs.Components.WeaponProcessor();
            }
            else if (spec.Data is JunkyardDogs.Specifications.SubProcessor)
            {
                component = new JunkyardDogs.Components.SubProcessor();
            }
            else if (spec.Data is JunkyardDogs.Specifications.Plate)
            {
                component = new JunkyardDogs.Components.Plate();
            }
            else if (spec.Data is JunkyardDogs.Specifications.CPU)
            {
                component = new JunkyardDogs.Components.CPU();
            }
            else if (spec.Data is JunkyardDogs.Specifications.CircuitBoard)
            {
                component = new JunkyardDogs.Components.CircuitBoard();
            }

            component.SpecificationReference = spec;
            return component;
        }
    }
}