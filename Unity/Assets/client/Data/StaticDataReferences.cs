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
        public static implicit operator Nationality(NationalityStaticDataReference reference) { return reference.Data; }
    }
    
    [Serializable]
    public class SpecificationStaticDataReference : StaticDataReference<Specification,Specification, SpecificationStaticDataReference, SpecificationDataProvider>
    {
        public static implicit operator Specification(SpecificationStaticDataReference reference) { return reference.Data; }
    }
    
    [Serializable]
    public class ManufacturerStaticDataReference : StaticDataReference<Manufacturer, Manufacturer, ManufacturerStaticDataReference, ManufacturerDataProvider>
    {
        public static implicit operator Manufacturer(ManufacturerStaticDataReference reference) { return reference.Data; }
    }
    
    [Serializable]
    public class MaterialStaticDataReference : StaticDataReference<Material, Material, MaterialStaticDataReference, MaterialDataProvider>
    {
        public static implicit operator Material(MaterialStaticDataReference reference) { return reference.Data; }
    }
    
    [Serializable]
    public class CompetitorBlueprintStaticDataReference : StaticDataReference<BlueprintDataBase, CompetitorBlueprintData, CompetitorBlueprintStaticDataReference, CompetitorBlueprintDataProvider>
    {
        public static implicit operator CompetitorBlueprintData(CompetitorBlueprintStaticDataReference reference) { return reference.Data; }
    }
    
    [Serializable]
    public class BlueprintStaticDataReference : StaticDataReference<BlueprintDataBase, BlueprintDataBase, BlueprintStaticDataReference, BlueprintDataProvider>
    {
        public static implicit operator BlueprintDataBase(BlueprintStaticDataReference reference) { return reference.Data; }
    }
    
    [Serializable]
    public class WeaponBlueprintStaticDataReference : StaticDataReference<BlueprintDataBase, WeaponBlueprintData, WeaponBlueprintStaticDataReference, WeaponBlueprintDataProvider>
    {
        public static implicit operator WeaponBlueprintData(WeaponBlueprintStaticDataReference reference) { return reference.Data; }
    }
    
    [Serializable]
    public class AgentBlueprintStaticDataReference : StaticDataReference<BlueprintDataBase, AgentBlueprintData, AgentBlueprintStaticDataReference, AgentBlueprintDataProvider>
    {
        public static implicit operator AgentBlueprintData(AgentBlueprintStaticDataReference reference) { return reference.Data; }
    }
    
    [Serializable]
    public class DirectiveBlueprintStaticDataReference : StaticDataReference<BlueprintDataBase, DirectiveBlueprintData, DirectiveBlueprintStaticDataReference, DirectiveBlueprintDataProvider>
    {
        public static implicit operator DirectiveBlueprintData(DirectiveBlueprintStaticDataReference reference) { return reference.Data; }
    }
    
    [Serializable]
    public class PlateBlueprintStaticDataReference : StaticDataReference<BlueprintDataBase, PlateBlueprintData, PlateBlueprintStaticDataReference, PlateBlueprintDataProvider>
    {
        public static implicit operator PlateBlueprintData(PlateBlueprintStaticDataReference reference) { return reference.Data; }
    }
    
    [Serializable]
    public class MotherboardBlueprintStaticDataReference : StaticDataReference<BlueprintDataBase, MotherboardBlueprintData, MotherboardBlueprintStaticDataReference, MotherboardBlueprintDataProvider>
    {
        public static implicit operator MotherboardBlueprintData(MotherboardBlueprintStaticDataReference reference) { return reference.Data; }
    }
    
    [Serializable]
    public class BotBlueprintStaticDataReference : StaticDataReference<BlueprintDataBase, BotBlueprintData, BotBlueprintStaticDataReference, BotBlueprintDataProvider>
    {
        public static implicit operator BotBlueprintData(BotBlueprintStaticDataReference reference) { return reference.Data; }
    }
    
    [Serializable]
    public class WeaponProcessorBlueprintStaticDataReference : StaticDataReference<BlueprintDataBase, WeaponProcessorBlueprintData, WeaponProcessorBlueprintStaticDataReference, WeaponProcessorBlueprintDataProvider>
    {
        public static implicit operator WeaponProcessorBlueprintData(WeaponProcessorBlueprintStaticDataReference reference) { return reference.Data; }
    }
    
    [Serializable]
    public class ChassisBlueprintStaticDataReference : StaticDataReference<BlueprintDataBase, ChassisBlueprintData, ChassisBlueprintStaticDataReference, ChassisBlueprintDataProvider>
    {
        public static implicit operator ChassisBlueprintData(ChassisBlueprintStaticDataReference reference) { return reference.Data; }
    }
    
    [Serializable]
    public class ParticipantStaticDataReference : StaticDataReference<ParticipantData, ParticipantData, ParticipantStaticDataReference, ParticipantDataProvider>
    {
        public static implicit operator ParticipantData(ParticipantStaticDataReference reference) { return reference.Data; }
    }
    
    [Serializable]
    public class WeaponStaticDataReference : StaticDataReference<Specification, Weapon, WeaponStaticDataReference, WeaponDataProvider>
    {
        public static implicit operator Weapon(WeaponStaticDataReference reference) { return reference.Data; }
    }
    
    [Serializable]
    public class EngineBlueprintStaticDataReference : StaticDataReference<BlueprintDataBase, EngineBlueprintData, EngineBlueprintStaticDataReference, EngineBlueprintDataProvider>
    {
        public static implicit operator EngineBlueprintData(EngineBlueprintStaticDataReference reference) { return reference.Data; }
    }
    
    [Serializable]
    public class CPUStaticDataReference : StaticDataReference<Specification, CPU, CPUStaticDataReference, CPUDataProvider>
    {
        public static implicit operator CPU(CPUStaticDataReference reference) { return reference.Data; }
    }
    
    [Serializable]
    public class StateKnowledgeStaticDataReference : StaticDataReference<State, State, StateKnowledgeStaticDataReference, StateKnowledgeStaticDataProvider>
    {
        public static implicit operator State(StateKnowledgeStaticDataReference reference) { return reference.Data; }
    }

    [Serializable]
    public class ActionStaticDataReference : StaticDataReference<BehaviorAction, BehaviorAction, ActionStaticDataReference, ActionStaticDataProvider>
    {
        public static implicit operator BehaviorAction(ActionStaticDataReference reference) { return reference.Data; }
    }
    
    [Serializable]
    public class TournamentStaticDataReference : StaticDataReference<Tournament, Tournament, TournamentStaticDataReference, TournamentDataProvider>
    {
        public static implicit operator Tournament(TournamentStaticDataReference reference) { return reference.Data; }
    }
    
    [Serializable]
    public class TournamentFormatStaticDataReference : StaticDataReference<TournamentFormat, TournamentFormat, TournamentFormatStaticDataReference, TournamentFormatDataProvider>
    {
        public static implicit operator TournamentFormat(TournamentFormatStaticDataReference reference) { return reference.Data; }
    }
    
    [Serializable]
    public class StageFormatStaticDataReference : StaticDataReference<StageFormat, StageFormat, StageFormatStaticDataReference,StageFormatDataProvider>
    {
        public static implicit operator StageFormat(StageFormatStaticDataReference reference) { return reference.Data; }
    }
    
    [Serializable]
    public class LootStaticDataReference : StaticDataReference<AbstractLootData, AbstractLootData, LootStaticDataReference,LootDataProvider>
    {
        public static implicit operator AbstractLootData(LootStaticDataReference reference) { return reference.Data; }
    }
    
    [Serializable]
    public class LootCrateStaticDataReference : StaticDataReference<AbstractLootCrateData, AbstractLootCrateData, LootCrateStaticDataReference,LootCrateDataProvider>
    {
        public static implicit operator AbstractLootCrateData(LootCrateStaticDataReference reference) { return reference.Data; }
    }
    
    [Serializable]
    public class CurrencyStaticDataReference : StaticDataReference<CurrencyData, CurrencyData, CurrencyStaticDataReference,CurrencyDataProvider>
    {
        public static implicit operator CurrencyData(CurrencyStaticDataReference reference) { return reference.Data; }
    }
    
    [Serializable]
    public class BreakpointStaticDataReference : StaticDataReference<BreakpointData, BreakpointData, BreakpointStaticDataReference, BreakpointDataProvider>
    {
        public static implicit operator BreakpointData(BreakpointStaticDataReference reference) { return reference.Data; }
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
    
    public class EngineBlueprintStaticDataReferenceAttribute : StaticDataReferenceAttribute
    {
        public EngineBlueprintStaticDataReferenceAttribute() : base(BlueprintDataProvider.FULL_PATH, typeof(EngineBlueprintData))
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
    
    public class BreakpointStaticDataReferenceAttribute : StaticDataReferenceAttribute
    {
        public BreakpointStaticDataReferenceAttribute() : base(BreakpointDataProvider.FULL_PATH, typeof(BreakpointData))
        {
            
        }
    }
}

