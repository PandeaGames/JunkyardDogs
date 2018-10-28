﻿using JunkyardDogs.Simulation;
using UnityEngine;
using System.Collections.Generic;
using JunkyardDogs.Components;
using JunkyardDogs.Simulation.Simulation;

public class SimpleBotMatchRendering : MonoBehaviour
{
    [SerializeField] 
    private BotRenderConfiguration _botRenderConfiguration;
    
    [SerializeField]
    private ServiceManager _serviceManager;
    
    [SerializeField]
    private PrefabFactory _botPrefabFactory;

    private SimulationService _simulationService;
    private Dictionary<SimulatedBot, BotRenderer> _simulatedBots;
    
    private void Start()
    {
        _simulationService = _serviceManager.GetService<SimulationService>();
        _simulatedBots = new Dictionary<SimulatedBot, BotRenderer>();
    }

    private void OnDestroy()
    {
        foreach (KeyValuePair<SimulatedBot, BotRenderer> simulatedBot in _simulatedBots)
        {
            Destroy(simulatedBot.Value.gameObject);
        }
        
        _simulatedBots.Clear();
    }
    
    private void Update()
    {
        foreach (KeyValuePair<BotId, SimulatedBot> simulationServiceSimulatedBot in _simulationService.SimulatedBots)
        {
            if (!_simulatedBots.ContainsKey(simulationServiceSimulatedBot.Value))
            {
                Bot bot = _simulationService.GetBot(simulationServiceSimulatedBot.Key);
                GameObject botObject =
                    Instantiate(_botPrefabFactory.GetAsset(bot.Chassis.Specification));
                
                BotRenderer renderer = botObject.AddComponent<BotRenderer>();
                _simulatedBots.Add(simulationServiceSimulatedBot.Value, renderer);
                
                renderer.Render(simulationServiceSimulatedBot.Value.Bot, _botRenderConfiguration);
            }
        }

        foreach (KeyValuePair<SimulatedBot, BotRenderer> keyValuePair in _simulatedBots)
        {
            keyValuePair.Value.transform.position = keyValuePair.Key.collider.PositionToWorld();
        }
    }
}
