using System.Collections;
using JunkyardDogs.Components;
using JunkyardDogs.Simulation;
using UnityEngine;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;
using System.Collections.Generic;
using JunkyardDogs;
using JunkyardDogs.Data;
using PandeaGames;

public class TournamentTest : MonoBehaviour
{
    [SerializeField, TournamentStaticDataReference]
    public TournamentStaticDataReference _testData;

    private TournamentViewModel _viewModel;
    private TournamentTestViewController _vc;

    private void Start()
    {
        _viewModel = Game.Instance.GetViewModel<TournamentViewModel>(0);
        _viewModel.Tournament = _testData;
        _vc = new TournamentTestViewController();
        _vc.ShowView(); 
    }
    
    private void Update()
    {
        _vc.Update();
    }
}