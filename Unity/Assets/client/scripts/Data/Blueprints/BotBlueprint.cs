using JunkyardDogs.Components;
using UnityEngine;
using JunkyardDogs.Specifications;
using System;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

[Serializable]
public class BotBlueprint : Blueprint<Bot, BotBlueprintData>
{
    [SerializeField][WeakReference(typeof(Manufacturer))]
    private WeakReference _manufacturer;
    
    [SerializeField]
    private ChassisBlueprint _chassis; 
    
    [SerializeField]
    private MotherboardBlueprint _motherboard; 
    
    [SerializeField]
    private AgentBlueprint _agent; 

    protected override void DoGenerate(int seed, Action<Bot> onComplete, Action onError)
    {
        _chassis.Generate((chassis) =>
        {
            _agent.Generate((agent) =>
            {
                _motherboard.Generate((motherboard) =>
                {
                    chassis.Manufacturer = _manufacturer;

                    Bot bot = new Bot();

                    bot.Chassis = chassis as JunkyardDogs.Components.Chassis;
                    bot.Agent = agent;
                    
                    onComplete(bot);

                }, onError);
            }, onError);
        }, onError);
    }
}
