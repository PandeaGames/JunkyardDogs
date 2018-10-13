using System.Collections;
using JunkyardDogs.Components;
using JunkyardDogs.Simulation;
using UnityEngine;
using WeakReference = Data.WeakReference;
using System.Collections.Generic;

public class TournamentTest : MonoBehaviour
{
    [SerializeField, WeakReference(typeof(Tournament))]
    public WeakReference _tournament;
    
    [SerializeField, WeakReference(typeof(JunkyardDogs.Simulation.Knowledge.State))]
    private WeakReference _state;
    
    [SerializeField, WeakReference(typeof(ParticipantData))]
    public List<WeakReference> _participants;

    [SerializeField]
    private TournamentController _tournamentController;

    private void Start()
    {
        _tournamentController.RunTournament(_tournament, _participants, _state);
    }
}