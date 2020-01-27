using System;
using JunkyardDogs.Simulation;
using UnityEngine;
using JunkyardDogs.Components;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using JunkyardDogs;
using PandeaGames;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

public struct MatchOutcome
{
    private SimulationService.Outcome _simulationOutcome;
    private MatchState _match;
    private Participant _winner;
    private Participant _loser;

    public SimulationService.Outcome SimulationOutcome { get { return _simulationOutcome; } }
    public MatchState Match { get { return _match; } }
    public Participant Winner { get { return _winner; } }
    public Participant Loser { get { return _loser; } }

    public MatchOutcome(
        SimulationService.Outcome simulationOutcome, 
        MatchState match, 
        Participant winner, 
        Participant loser)
    {
        _simulationOutcome = simulationOutcome;
        _match = match;
        _winner = winner;
        _loser = loser;
    }
}

public class MatchController : MonoBehaviour, ISimulatedEngagementListener
{
    [SerializeField]
    private ServiceManager _serviceManager;

    [SerializeField] 
    private CameraAgent _cameraAgent;

    [SerializeField]
    private SimpleSimulatedEngagement _engagementView;
    
    [SerializeField] 
    private MatchUIBehaviour _matchUI;

    private SimulationRunnerBehaviour _simulationRunnerBehaviour;
    private List<ISimulationTestExporter> _dataExporters;
    private CameraViewModel _cameraViewModel;
    private Action<MatchOutcome> _onMatchComplete;
    private MatchState _match;
    private Engagement _engagement;
    private MatchViewModel _viewModel;
    private SimulatedEngagement _simulation;

    private void Start()
    {
        _dataExporters = SimulationDebugUtils.GetAllDataExporters();
        _viewModel = Game.Instance.GetViewModel<MatchViewModel>(0);
        _cameraViewModel = Game.Instance.GetViewModel<CameraViewModel>(0);
        _simulationRunnerBehaviour = gameObject.AddComponent<SimulationRunnerBehaviour>();
        
        //_simulationService.SetEngagement(_viewModel.Engagement);
       // _simulationService.StartSimulation();
        StartCoroutine(EndOfBattleCoroutine());
        _simulation = new SimulatedEngagement(_viewModel.Engagement, this);
        _matchUI.Setup(_simulation);
        _simulationRunnerBehaviour.Init(_simulation);

        //while (!simulation.Step());
        
        #if UNITY_EDITOR
        WriteSimulationResultsToDisc(_simulation);
        #endif
        
        _cameraViewModel.Focus(_cameraAgent);
    }
    
    private void OnError(LoadException error)
    {
        
    }

    private void OnDrawGizmos()
    {
        if (_simulation != null)
        {
            _simulation.OnDrawGizmos();
        }
    }

    private IEnumerator EndOfBattleCoroutine()
    {
        while (_viewModel.Engagement.Outcome == null)
        {
            yield return 0;
        }

        if (SimulationDebugUtils.GenerateSimulationDebugData)
        {
            Debug.Log($"[{nameof(MatchController)}] Exporting Simulation Data to PlayerPrefs");
            ExportSimulation(_simulation);
        }
        
        Debug.Log("WINNER");
        yield break;
    }
    
    private void ExportSimulation(SimulatedEngagement simulatedEngagement)
    {
        for (int i = 0; i < _dataExporters.Count; i++)
        {
            ISimulationTestExporter exporter = _dataExporters[i];
            
            SimulationTestExportData inputData = new SimulationTestExportData(simulatedEngagement, SimulationDebugUtils.InitiatorToDebug);
            
            StringBuilder data = exporter.GetData(inputData);
            string prefName =  exporter.GetDataName()+".data.playerpref";
            SimulationDebugUtils.SetSimulationDebugData(prefName, data.ToString());
            Debug.Log($"[{nameof(MatchController)}] Exporting Simulation Data to PlayerPrefs [{prefName}]");
        }
    }

    public void StepStart()
    {
        
    }

    public void StepComplete()
    {
        
    }

    public void OnEvent(SimulatedEngagement simulatedEngagement, SimEvent e)
    {
        _engagementView.OnSimEvent(simulatedEngagement, e);
    }

    private void WriteSimulationResultsToDisc(SimulatedEngagement simulatedEngagement)
    {
        
    }
}
