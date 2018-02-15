using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JunkyardDogs.Simulation;

public class SimulationTests : MonoBehaviour {

    [SerializeField]
    private Engagement _engagement;

    [SerializeField]
    private ServiceManager _serviceManager;

    private SimulationService _simulationService;

    // Use this for initialization
    void Start () {
        _simulationService = _serviceManager.GetService<SimulationService>();
        _simulationService.SetEngagement(_engagement);
        _simulationService.SetSimulationSpeed(1);
        _simulationService.StartSimulation();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
