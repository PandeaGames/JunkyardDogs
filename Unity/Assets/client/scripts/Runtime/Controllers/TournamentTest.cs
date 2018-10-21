using System.Collections;
using JunkyardDogs.Components;
using JunkyardDogs.Simulation;
using UnityEngine;
using WeakReference = Data.WeakReference;
using System.Collections.Generic;

public class TournamentTest : MonoBehaviour
{
    [SerializeField]
    public TournamentTestData _testData;

    [SerializeField]
    private TournamentController _tournamentController;

    private void Start()
    {
        _tournamentController.RunTournament(_testData.Tournament, _testData.Participants, _testData.State);
    }
}