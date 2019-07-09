using System;
using JunkyardDogs.Simulation.Knowledge;
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
    public class CompetitorBlueprintStaticDataReference : StaticDataReference<BlueprintDataBase, CompetitorBlueprintData, CompetitorBlueprintStaticDataReference, CompetitorBlueprintDataProvider>
    {
    }
    
    [Serializable]
    public class BlueprintStaticDataReference : StaticDataReference<BlueprintDataBase, BlueprintDataBase, BlueprintStaticDataReference, BlueprintDataProvider>
    {
    }
    
    [Serializable]
    public class WeaponBlueprintStaticDataReference : StaticDataReference<BlueprintDataBase, WeaponBlueprintData, WeaponBlueprintStaticDataReference, WeaponBlueprintDataProvider>
    {
    }
    
    [Serializable]
    public class AgentBlueprintStaticDataReference : StaticDataReference<BlueprintDataBase, AgentBlueprintData, AgentBlueprintStaticDataReference, AgentBlueprintDataProvider>
    {
    }
    
    [Serializable]
    public class PlateBlueprintStaticDataReference : StaticDataReference<BlueprintDataBase, PlateBlueprintData, PlateBlueprintStaticDataReference, PlateBlueprintDataProvider>
    {
    }
    
    [Serializable]
    public class MotherboardBlueprintStaticDataReference : StaticDataReference<BlueprintDataBase, MotherboardBlueprintData, MotherboardBlueprintStaticDataReference, MotherboardBlueprintDataProvider>
    {
    }
    
    [Serializable]
    public class BotBlueprintStaticDataReference : StaticDataReference<BlueprintDataBase, BotBlueprintData, BotBlueprintStaticDataReference, BotBlueprintDataProvider>
    {
    }
    
    [Serializable]
    public class WeaponProcessorBlueprintStaticDataReference : StaticDataReference<BlueprintDataBase, WeaponProcessorBlueprintData, WeaponProcessorBlueprintStaticDataReference, WeaponProcessorBlueprintDataProvider>
    {
    }
    
    [Serializable]
    public class ChassisBlueprintStaticDataReference : StaticDataReference<BlueprintDataBase, ChassisBlueprintData, ChassisBlueprintStaticDataReference, ChassisBlueprintDataProvider>
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
    
    [Serializable]
    public class CPUStaticDataReference : StaticDataReference<Specification, CPU, CPUStaticDataReference, CPUDataProvider>
    {
    }
    
    [Serializable]
    public class StateKnowledgeStaticDataReference : StaticDataReference<State, State, StateKnowledgeStaticDataReference, StateKnowledgeStaticDataProvider>
    {
    }

    [Serializable]
    public class ActionStaticDataReference : StaticDataReference<BehaviorAction, BehaviorAction, ActionStaticDataReference, ActionStaticDataProvider>
    {
    }
    
    [Serializable]
    public class TournamentStaticDataReference : StaticDataReference<Tournament, Tournament, TournamentStaticDataReference, TournamentDataProvider>
    {
    }
    
    [Serializable]
    public class TournamentFormatStaticDataReference : StaticDataReference<TournamentFormat, TournamentFormat, TournamentFormatStaticDataReference, TournamentFormatDataProvider>
    {
    }
    
    [Serializable]
    public class StageFormatStaticDataReference : StaticDataReference<StageFormat, StageFormat, StageFormatStaticDataReference,StageFormatDataProvider>
    {
    }
    
    [Serializable]
    public class LootStaticDataReference : StaticDataReference<AbstractLootData, AbstractLootData, LootStaticDataReference,LootDataProvider>
    {
    }
    
    [Serializable]
    public class LootCrateStaticDataReference : StaticDataReference<AbstractLootCrateData, AbstractLootCrateData, LootCrateStaticDataReference,LootCrateDataProvider>
    {
    }
    
    [Serializable]
    public class CurrencyStaticDataReference : StaticDataReference<CurrencyData, CurrencyData, CurrencyStaticDataReference,CurrencyDataProvider>
    {
    }
    
    /*------------------------------------------------------------------*/
    /*----------------------------- Attributes -------------------------*/
    /*------------------------------------------------------------------*/
    
    public class BlueprintStaticDataReferenceAttribute : StaticDataReferenceAttribute
    {
        public BlueprintStaticDataReferenceAttribute() : base(BlueprintDataProvider.FULL_PATH, typeof(BlueprintDataBase))
        {
            
        }
    }
    
    public class CPUStaticDataReferenceAttribute : StaticDataReferenceAttribute
    {
        public CPUStaticDataReferenceAttribute() : base(SpecificationDataProvider.FULL_PATH, typeof(CPU))
        {
            
        }
    }
    
    public class DirectiveStaticDataReferenceAttribute : StaticDataReferenceAttribute
    {
        public DirectiveStaticDataReferenceAttribute() : base(SpecificationDataProvider.FULL_PATH, typeof(Specifications.Directive))
        {
            
        }
    }
    
    public class BotBlueprintStaticDataReferenceAttribute : StaticDataReferenceAttribute
    {
        public BotBlueprintStaticDataReferenceAttribute() : base(BlueprintDataProvider.FULL_PATH, typeof(BotBlueprintData))
        {
            
        }
    }
    
    public class MotherboardBlueprintStaticDataReferenceAttribute : StaticDataReferenceAttribute
    {
        public MotherboardBlueprintStaticDataReferenceAttribute() : base(BlueprintDataProvider.FULL_PATH, typeof(MotherboardBlueprintData))
        {
            
        }
    }
    
    public class ChassisBlueprintStaticDataReferenceAttribute : StaticDataReferenceAttribute
    {
        public ChassisBlueprintStaticDataReferenceAttribute() : base(BlueprintDataProvider.FULL_PATH, typeof(ChassisBlueprintData))
        {
            
        }
    }
    
    public class PlateBlueprintStaticDataReferenceAttribute : StaticDataReferenceAttribute
    {
        public PlateBlueprintStaticDataReferenceAttribute() : base(BlueprintDataProvider.FULL_PATH, typeof(PlateBlueprintData))
        {
            
        }
    }
    
    public class AgentBlueprintStaticDataReferenceAttribute : StaticDataReferenceAttribute
    {
        public AgentBlueprintStaticDataReferenceAttribute() : base(BlueprintDataProvider.FULL_PATH, typeof(AgentBlueprintData))
        {
            
        }
    }
    
    public class WeaponBlueprintStaticDataReferenceAttribute : StaticDataReferenceAttribute
    {
        public WeaponBlueprintStaticDataReferenceAttribute() : base(BlueprintDataProvider.FULL_PATH, typeof(WeaponBlueprintData))
        {
            
        }
    }
    
    public class CompetitorBlueprintStaticDataReferenceAttribute : StaticDataReferenceAttribute
    {
        public CompetitorBlueprintStaticDataReferenceAttribute() : base(BlueprintDataProvider.FULL_PATH, typeof(CompetitorBlueprintData))
        {
            
        }
    }
    
    public class WeaponProcessorBlueprintStaticDataReferenceAttribute : StaticDataReferenceAttribute
    {
        public WeaponProcessorBlueprintStaticDataReferenceAttribute() : base(BlueprintDataProvider.FULL_PATH, typeof(WeaponProcessorBlueprintData))
        {
            
        }
    }
    
    public class StateKnowledgeStaticDataReferenceAttribute : StaticDataReferenceAttribute
    {
        public StateKnowledgeStaticDataReferenceAttribute() : base(StateKnowledgeStaticDataProvider.FULL_PATH, typeof(State))
        {
            
        }
    }
    
    public class ParticipantStaticDataReferenceAttribute : StaticDataReferenceAttribute
    {
        public ParticipantStaticDataReferenceAttribute() : base(ParticipantDataProvider.FULL_PATH, typeof(ParticipantData))
        {
            
        }
    }
    
    public class MaterialStaticDataReferenceAttribute : StaticDataReferenceAttribute
    {
        public MaterialStaticDataReferenceAttribute() : base(MaterialDataProvider.FULL_PATH, typeof(Material))
        {
            
        }
    }
    
    public class ActionStaticDataReferenceAttribute : StaticDataReferenceAttribute
    {
        public ActionStaticDataReferenceAttribute() : base(ActionStaticDataProvider.FULL_PATH, typeof(BehaviorAction))
        {
            
        }
    }
    
    public class TournamentStaticDataReferenceAttribute : StaticDataReferenceAttribute
    {
        public TournamentStaticDataReferenceAttribute() : base(TournamentDataProvider.FULL_PATH, typeof(Tournament))
        {
            
        }
    }
    
    public class TournamentFormatStaticDataReferenceAttribute : StaticDataReferenceAttribute
    {
        public TournamentFormatStaticDataReferenceAttribute() : base(TournamentFormatDataProvider.FULL_PATH, typeof(TournamentFormat))
        {
            
        }
    }
    
    public class StageFormatStaticDataReferenceAttribute : StaticDataReferenceAttribute
    {
        public StageFormatStaticDataReferenceAttribute() : base(StageFormatDataProvider.FULL_PATH, typeof(StageFormat))
        {
            
        }
    }
    
    public class LootStaticDataReferenceAttribute : StaticDataReferenceAttribute
    {
        public LootStaticDataReferenceAttribute() : base(LootDataProvider.FULL_PATH, typeof(AbstractLootData))
        {
            
        }
    }
    
    public class LootCrateStaticDataReferenceAttribute : StaticDataReferenceAttribute
    {
        public LootCrateStaticDataReferenceAttribute() : base(LootCrateDataProvider.FULL_PATH, typeof(AbstractLootCrateData))
        {
            
        }
    }
    
    public class CurrencyStaticDataReferenceAttribute : StaticDataReferenceAttribute
    {
        public CurrencyStaticDataReferenceAttribute() : base(CurrencyDataProvider.FULL_PATH, typeof(CurrencyData))
        {
            
        }
    }
}

