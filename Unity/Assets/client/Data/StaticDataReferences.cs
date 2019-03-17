using System;
using JunkyardDogs.Specifications;
using PandeaGames.Data.Static;
using UnityEngine;
using Material = JunkyardDogs.Specifications.Material;

namespace JunkyardDogs.Data
{
    [Serializable]
    public class NationalityStaticDataReference : StaticDataReference<Nationality, NationalityStaticDataReference, NationalityDataProvider>
    {
    }
    
    [Serializable]
    public class SpecificationStaticDataReference : StaticDataReference<Specification, SpecificationStaticDataReference, SpecificationDataProvider>
    {
    }
    
    [Serializable]
    public class ManufacturerStaticDataReference : StaticDataReference<Manufacturer, ManufacturerStaticDataReference, ManufacturerDataProvider>
    {
    }
    
    [Serializable]
    public class MaterialStaticDataReference : StaticDataReference<Material, MaterialStaticDataReference, MaterialDataProvider>
    {
    }
    
    [Serializable]
    public class CompetitorBlueprintStaticDataReference : StaticDataReference<CompetitorBlueprintData, CompetitorBlueprintStaticDataReference, CompetitorBlueprintDataProvider>
    {
    }
    
    [Serializable]
    public class ParticipantStaticDataReference : StaticDataReference<ParticipantData, ParticipantStaticDataReference, ParticipantDataProvider>
    {
    }
}