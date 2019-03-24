using JunkyardDogs.Components;
using JunkyardDogs.Data;
using JunkyardDogs.Simulation.Agent;
using UnityEngine;

[CreateAssetMenu(menuName = "Blueprints/BotBlueprint")]
public class BotBlueprintData : BlueprintData<Bot>
{
    [SerializeField, StaticDataReference(ManufacturerDataProvider.FULL_PATH)]
    private ManufacturerStaticDataReference _manufacturer;
    
    [SerializeField, ChassisBlueprintStaticDataReference]
    private ChassisBlueprintStaticDataReference _chassis; 
    
    [SerializeField, MotherboardBlueprintStaticDataReference]
    private MotherboardBlueprintStaticDataReference _motherboard; 
    
    [SerializeField, AgentBlueprintStaticDataReference]
    private AgentBlueprintStaticDataReference _agent; 

    public override Bot DoGenerate(int seed)
    {
        Chassis chassis = _chassis.Data.DoGenerate(seed);
        Agent agent = _agent.Data.DoGenerate();
        Motherboard motherboard = _motherboard.Data.DoGenerate(seed);
        
        chassis.Manufacturer = _manufacturer;

        Bot bot = new Bot();

        bot.Chassis = chassis;
        bot.Agent = agent;

        return bot;
    }
}