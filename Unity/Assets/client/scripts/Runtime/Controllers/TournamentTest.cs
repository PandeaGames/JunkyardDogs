using System.Collections;
using JunkyardDogs.Components;
using JunkyardDogs.Simulation;
using UnityEngine;
using WeakReference = Data.WeakReference;
using System.Collections.Generic;

public class TournamentTest : MonoBehaviour
{
    [SerializeField, WeakReference(typeof(Tournament))]
    public WeakReference _testData;

    [SerializeField]
    private TournamentController _tournamentController;

    private void Start()
    {
        _tournamentController.RunTournament(_testData);
    }
}