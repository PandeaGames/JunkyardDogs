using UnityEngine;
using System.Collections;
using JunkyardDogs.Specifications;
using System;
using Polenter.Serialization;
using WeakReference = Data.WeakReference;
using System.Threading.Tasks;

namespace JunkyardDogs.Components
{
    public static class ComponentUtils
    {
        public static JunkyardDogs.Components.Component GenerateComponent(WeakReference spec)
        {
            JunkyardDogs.Components.Component component = null;
            ScriptableObject asset = spec.Asset;

            if (asset is JunkyardDogs.Specifications.Weapon)
            {
                component = new JunkyardDogs.Components.Weapon();
            }
            else if (asset is JunkyardDogs.Specifications.Chassis)
            {
                component = new JunkyardDogs.Components.Chassis();
            }
            else if (asset is JunkyardDogs.Specifications.WeaponChip)
            {
                component = new JunkyardDogs.Components.WeaponProcessor();
            }
            else if (asset is JunkyardDogs.Specifications.SubProcessor)
            {
                component = new JunkyardDogs.Components.SubProcessor();
            }
            else if (asset is JunkyardDogs.Specifications.Plate)
            {
                component = new JunkyardDogs.Components.Plate();
            }
            else if (asset is JunkyardDogs.Specifications.CPU)
            {
                component = new JunkyardDogs.Components.CPU();
            }
            else if (asset is JunkyardDogs.Specifications.CircuitBoard)
            {
                component = new JunkyardDogs.Components.CircuitBoard();
            }

            component.SpecificationReference = spec;

            return component;
        }
    }
}