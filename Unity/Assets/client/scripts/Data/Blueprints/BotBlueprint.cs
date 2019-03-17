using JunkyardDogs.Components;
using UnityEngine;
using JunkyardDogs.Specifications;
using System;
using JunkyardDogs.Data;
using JunkyardDogs.Simulation.Agent;
using Chassis = JunkyardDogs.Components.Chassis;
using Motherboard = JunkyardDogs.Components.Motherboard;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

[Serializable]
public class BotBlueprint : Blueprint<Bot, BotBlueprintData>
{
    [SerializeField]
    private ManufacturerStaticDataReference _manufacturer;
    
    [SerializeField]
    private ChassisBlueprint _chassis; 
    
    [SerializeField]
    private MotherboardBlueprint _motherboard; 
    
    [SerializeField]
    private AgentBlueprint _agent; 

    protected override Bot DoGenerate(int seed)
    {
        Chassis chassis = (Chassis)_chassis.Generate();
        Agent agent = (Agent) _agent.Generate();
        Motherboard motherboard = (Motherboard)_motherboard.Generate();
        
        chassis.Manufacturer = _manufacturer;

        Bot bot = new Bot();

        bot.Chassis = chassis;
        bot.Agent = agent;

        return bot;
    }
}
