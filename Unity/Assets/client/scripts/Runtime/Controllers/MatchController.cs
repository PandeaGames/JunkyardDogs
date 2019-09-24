using System;
using JunkyardDogs.Simulation;
using UnityEngine;
using JunkyardDogs.Components;
using System.Collections.Generic;
using System.Collections;
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

    private SimulationService _simulationService;
    private CameraViewModel _cameraViewModel;
    private Action<MatchOutcome> _onMatchComplete;
    private MatchState _match;
    private Engagement _engagement;
    private MatchViewModel _viewModel;
    private SimulatedEngagement _simulation;

    private void Start()
    {
        _viewModel = Game.Instance.GetViewModel<MatchViewModel>(0);
        _simulationService = _serviceManager.GetService<SimulationService>();
        _cameraViewModel = Game.Instance.GetViewModel<CameraViewModel>(0);
        
        //_simulationService.SetEngagement(_viewModel.Engagement);
       // _simulationService.StartSimulation();
        StartCoroutine(EndOfBattleCoroutine());
        _simulation = new SimulatedEngagement(_viewModel.Engagement, this);
        _matchUI.Setup(_simulation);

        //while (!simulation.Step());
        
        #if UNITY_EDITOR
        WriteSimulationResultsToDisc(_simulation);
        #endif
        
        _cameraViewModel.Focus(_cameraAgent);
    }
    
    private void OnError(LoadException error)
    {
        
    }

    private void Update()
    {
        if(!_simulation.Step())
        {
            Debug.Log("WINNER");
        }  
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
       
        Debug.Log("WINNER");
        yield break;
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
