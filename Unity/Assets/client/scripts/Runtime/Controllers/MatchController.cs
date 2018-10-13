using System;
using JunkyardDogs.Simulation;
using UnityEngine;
using JunkyardDogs.Components;
using System.Collections.Generic;
using System.Collections;
using WeakReference = Data.WeakReference;

public class MatchController : MonoBehaviour
{
    [SerializeField]
    private ServiceManager _serviceManager;

    [SerializeField] 
    private CameraAgent _cameraAgent;

    private SimulationService _simulationService;
    private CameraService _cameraService;
    private Action _onMatchComplete;
    private MatchState _match;
    private WeakReference _state;
    private Engagement _engagement;
    private JunkyardUserService _userService;

    private void Awake()
    {
        _simulationService = _serviceManager.GetService<SimulationService>();
        _cameraService = _serviceManager.GetService<CameraService>();
        _userService = _serviceManager.GetService<JunkyardUserService>();
    }
    
    public void RunMatch(MatchState match, Action onMatchComplete, WeakReference state)
    {
        _state = state;
        _match = match;
        _onMatchComplete = onMatchComplete;
        _cameraService.Focus(_cameraAgent);
        
        _match.ParticipantA.Participant.GetTeam(_userService.Load(), teamA =>
        {
            _match.ParticipantB.Participant.GetTeam(_userService.Load(), teamB =>
            {
                _engagement = new Engagement();

                _engagement.BlueCombatent = teamA.Bot;
                _engagement.RedCombatent = teamB.Bot;
                _engagement.SetTimeLimit(180);//3 minutes
                
                PrepareForBattle(teamA.Bot);
                PrepareForBattle(teamB.Bot);
                
                _engagement.BlueCombatent.LoadAsync(() => _engagement.RedCombatent.LoadAsync(() =>
                {
                    _simulationService.SetEngagement(_engagement);
                    
                    _simulationService.StartSimulation(true);
                    StartCoroutine(EndOfBattleCoroutine());
                }, OnError), OnError);
                
            }, OnError);
        }, OnError);
    }
    
    private void OnError()
    {
        
    }
    
    private void PrepareForBattle(Bot bot)
    {
        bot.Agent.States.ForEach((state) => { state.StateWeakReference = _state; });
    }

    private IEnumerator EndOfBattleCoroutine()
    {
        while (_engagement.Outcome == null)
        {
            yield return 0;
        }
        
        if (_engagement.Outcome.Winner == _engagement.RedCombatent)
        {
            Debug.Log("WINNER: RED");
        }
        else
        {
            Debug.Log("WINNER: Blue");
        }
    }
}
