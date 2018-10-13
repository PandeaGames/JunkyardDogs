using JunkyardDogs.Simulation;
using UnityEngine;
using System.Collections.Generic;
using JunkyardDogs.Components;
using JunkyardDogs.Simulation.Simulation;

public class SimpleBotMatchRendering : MonoBehaviour
{
    [SerializeField]
    private ServiceManager _serviceManager;
    
    [SerializeField]
    private PrefabFactory _botPrefabFactory;

    [SerializeField]
    private PrefabFactory _componentFactory;
    
    [SerializeField]
    private ScriptableObjectFactory _avatarFactory;
    
    [SerializeField]
    private MaterialFactory _materials;

    [SerializeField]
    private UnityEngine.Material missingComponentMaterial;

    private SimulationService _simulationService;
    private Dictionary<SimulatedBot, BotRenderer> _simulatedBots;
    
    private void Start()
    {
        _simulationService = _serviceManager.GetService<SimulationService>();
        _simulatedBots = new Dictionary<SimulatedBot, BotRenderer>();
    }
    
    private void Update()
    {
        foreach (KeyValuePair<Bot, SimulatedBot> simulationServiceSimulatedBot in _simulationService.SimulatedBots)
        {
            if (!_simulatedBots.ContainsKey(simulationServiceSimulatedBot.Value))
            {
                GameObject botObject =
                    Instantiate(_botPrefabFactory.GetAsset(simulationServiceSimulatedBot.Key.Chassis.Specification));
                
                BotRenderer renderer = botObject.AddComponent<BotRenderer>();
                _simulatedBots.Add(simulationServiceSimulatedBot.Value, renderer);
                
                renderer.Render(simulationServiceSimulatedBot.Value.Bot,
                    _componentFactory,
                    _avatarFactory,
                    _materials, 
                    this.missingComponentMaterial);
            }
        }

        foreach (KeyValuePair<SimulatedBot, BotRenderer> keyValuePair in _simulatedBots)
        {
            keyValuePair.Value.transform.position = keyValuePair.Key.collider.PositionToWorld();
        }
    }
}
