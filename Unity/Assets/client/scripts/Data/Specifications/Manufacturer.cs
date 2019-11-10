using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JunkyardDogs.Components;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using Component = JunkyardDogs.Components.Component;
using JunkyardDogs.Simulation;

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
    public class Manufacturer : AbstractStaticData, IStaticDataBalance<ManufacturerBalanceObject>
    {
        [SerializeField]
        private List<Simulation.Distinction> _distinctions;

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
            name = balance.name;
            _nationality.ID = balance.nationality;

            _distinctions = new List<Simulation.Distinction>();

            ImportDistinction(_distinctions, balance.distinctionId_01, balance.distinctionValue_01);
            ImportDistinction(_distinctions, balance.distinctionId_02, balance.distinctionValue_02);
            ImportDistinction(_distinctions, balance.distinctionId_03, balance.distinctionValue_03);
            ImportDistinction(_distinctions, balance.distinctionId_04, balance.distinctionValue_04);
            ImportDistinction(_distinctions, balance.distinctionId_05, balance.distinctionValue_05);

        }

        private void ImportDistinction(List<Simulation.Distinction> list, string distinctionId, int distinctionValue)
        {
            if (!string.IsNullOrEmpty(distinctionId))
            {
                Simulation.Distinction Distinction = new Simulation.Distinction();
                Distinction.Type = (DistinctionType) Enum.Parse(typeof (DistinctionType), distinctionId);
                Distinction.Value = distinctionValue;
                list.Add(Distinction);
            }
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