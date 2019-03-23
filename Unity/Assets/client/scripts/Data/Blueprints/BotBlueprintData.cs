using JunkyardDogs.Components;
using JunkyardDogs.Data;
using JunkyardDogs.Simulation.Agent;
using UnityEngine;

[CreateAssetMenu(menuName = "Blueprints/BotBlueprint")]
public class BotBlueprintData : BlueprintData<Bot>
{
    [SerializeField, StaticDataReference(ManufacturerDataProvider.FULL_PATH)]
    private ManufacturerStaticDataReference _manufacturer;
    
    [SerializeField]
    private ChassisBlueprintData _chassis; 
    
    [SerializeField]
    private MotherboardBlueprintData _motherboard; 
    
    [SerializeField]
    private AgentBlueprintData _agent; 

    public override Bot DoGenerate(int seed)
    {
        Chassis chassis = _chassis.DoGenerate(seed);
        Agent agent = _agent.DoGenerate();
        Motherboard motherboard = _motherboard.DoGenerate(seed);
        
        chassis.Manufacturer = _manufacturer;

        Bot bot = new Bot();

        bot.Chassis = chassis;
        bot.Agent = agent;

        return bot;
    }
}