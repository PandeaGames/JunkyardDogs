﻿using JunkyardDogs.Components;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using JunkyardDogs.Simulation.Agent;
using UnityEngine;

[CreateAssetMenu(menuName = "Blueprints/BotBlueprint")]
public class BotBlueprintData : BlueprintData<Bot>, IStaticDataBalance<BotBlueprintBalanceObject>
{
    [SerializeField, StaticDataReference(ManufacturerDataProvider.FULL_PATH)]
    private ManufacturerStaticDataReference _manufacturer;
    
    [SerializeField, ChassisBlueprintStaticDataReference]
    private ChassisBlueprintStaticDataReference _chassis; 
    
    [SerializeField, MotherboardBlueprintStaticDataReference]
    private MotherboardBlueprintStaticDataReference _motherboard; 
    
    [SerializeField, AgentBlueprintStaticDataReference]
    private AgentBlueprintStaticDataReference _agent; 
    
    [SerializeField, CPUStaticDataReference]
    private SpecificationStaticDataReference _cpu;

    [SerializeField, DirectiveStaticDataReference]
    private SpecificationStaticDataReference _directive01;
    
    [SerializeField, DirectiveStaticDataReference]
    private SpecificationStaticDataReference _directive02;
    
    [SerializeField, DirectiveStaticDataReference]
    private SpecificationStaticDataReference _directive03;

    public override Bot DoGenerate(int seed)
    {
        Chassis chassis = _chassis.Data.DoGenerate(seed);
        Agent agent = _agent.Data.DoGenerate();
        Motherboard motherboard = _motherboard.Data.DoGenerate(seed);
        CPU cpu = (CPU) ComponentUtils.GenerateComponent(_cpu, _manufacturer);
        Directive directive01 = (Directive) ComponentUtils.GenerateComponent(_directive01, _manufacturer);
        Directive directive02 = (Directive) ComponentUtils.GenerateComponent(_directive02, _manufacturer);
        Directive directive03 = (Directive) ComponentUtils.GenerateComponent(_directive03, _manufacturer);

        chassis.Manufacturer = _manufacturer;

        Bot bot = new Bot();
        bot.Motherboard = motherboard;
        bot.CPU = cpu;

        bot.Chassis = chassis;
        bot.Agent = agent;
        
        cpu.Directives = new Directive[3];
        cpu.Directives[0] = directive01;
        cpu.Directives[1] = directive02;
        cpu.Directives[2] = directive03;

        return bot;
    }

    public void ApplyBalance(BotBlueprintBalanceObject balance)
    {
        name = balance.name;
        
        _manufacturer = new ManufacturerStaticDataReference();
        _chassis = new ChassisBlueprintStaticDataReference();
        _motherboard = new MotherboardBlueprintStaticDataReference();
        _agent = new AgentBlueprintStaticDataReference();

        _manufacturer.ID = balance.manufacturer;
        _chassis.ID = balance.chassis;
        _motherboard.ID = balance.motherboard;
        _agent.ID = balance.agent;
        
        
    }

    public BotBlueprintBalanceObject GetBalance()
    {
        BotBlueprintBalanceObject balance = new BotBlueprintBalanceObject();

        balance.name = name;
        balance.manufacturer = _manufacturer == null ? string.Empty : _manufacturer.ID;
        balance.chassis = _chassis == null ? string.Empty : _chassis.ID;
        balance.motherboard = _motherboard == null ? string.Empty : _motherboard.ID;
        balance.agent = _agent == null ? string.Empty : _agent.ID;

        return balance;
    }
}