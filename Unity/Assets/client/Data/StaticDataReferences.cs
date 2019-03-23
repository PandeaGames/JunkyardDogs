using System;
using JunkyardDogs.Specifications;
using PandeaGames.Data.Static;
using UnityEngine;
using Material = JunkyardDogs.Specifications.Material;

namespace JunkyardDogs.Data
{
    [Serializable]
    public class NationalityStaticDataReference : StaticDataReference<Nationality, Nationality, NationalityStaticDataReference, NationalityDataProvider>
    {
    }
    
    [Serializable]
    public class SpecificationStaticDataReference : StaticDataReference<Specification,Specification, SpecificationStaticDataReference, SpecificationDataProvider>
    {
    }
    
    [Serializable]
    public class ManufacturerStaticDataReference : StaticDataReference<Manufacturer, Manufacturer, ManufacturerStaticDataReference, ManufacturerDataProvider>
    {
    }
    
    [Serializable]
    public class MaterialStaticDataReference : StaticDataReference<Material, Material, MaterialStaticDataReference, MaterialDataProvider>
    {
    }
    
    [Serializable]
    public class CompetitorBlueprintStaticDataReference : StaticDataReference<CompetitorBlueprintData, CompetitorBlueprintData, CompetitorBlueprintStaticDataReference, CompetitorBlueprintDataProvider>
    {
    }
    
    [Serializable]
    public class ParticipantStaticDataReference : StaticDataReference<ParticipantData, ParticipantData, ParticipantStaticDataReference, ParticipantDataProvider>
    {
    }
    
    [Serializable]
    public class WeaponStaticDataReference : StaticDataReference<Specification, Weapon, WeaponStaticDataReference, WeaponDataProvider>
    {
    }
}