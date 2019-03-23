using System;
using UnityEngine;
using System.Collections;
using JunkyardDogs.Components;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
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
    public class Manufacturer : ScriptableObject, IStaticDataBalance<ManufacturerBalanceObject>
    {
        [SerializeField]
        private Distinction _distinctions;

        [SerializeField, StaticDataReference(NationalityDataProvider.FULL_PATH)]
        private NationalityStaticDataReference _nationality;
        public NationalityStaticDataReference nationality
        {
            get
            {
                if (_nationality == null)
                {
                    _nationality = new NationalityStaticDataReference();
                }

                return _nationality;
            }
        }
        
        public void ApplyBalance(ManufacturerBalanceObject balance)
        {
            this.name = balance.name;
            _nationality.ID = balance.nationality;
        }

        public ManufacturerBalanceObject GetBalance()
        {
            ManufacturerBalanceObject balance = new ManufacturerBalanceObject();
            balance.name = this.name;
            balance.nationality = nationality.ID;
            return balance;
        }
    }
}