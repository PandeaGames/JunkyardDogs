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

public class MatchController : MonoBehaviour
{
    [SerializeField]
    private ServiceManager _serviceManager;

    [SerializeField] 
    private CameraAgent _cameraAgent;

    private SimulationService _simulationService;
    private CameraViewModel _cameraViewModel;
    private Action<MatchOutcome> _onMatchComplete;
    private MatchState _match;
    private Engagement _engagement;
    private MatchViewModel _viewModel;

    private void Start()
    {
        _viewModel = Game.Instance.GetViewModel<MatchViewModel>(0);
        _simulationService = _serviceManager.GetService<SimulationService>();
        _cameraViewModel = Game.Instance.GetViewModel<CameraViewModel>(0);
        
        _simulationService.SetEngagement(_viewModel.Engagement);
        _simulationService.StartSimulation();
        StartCoroutine(EndOfBattleCoroutine());
        
        _cameraViewModel.Focus(_cameraAgent);
    }
    
    private void OnError(LoadException error)
    {
        
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
}
