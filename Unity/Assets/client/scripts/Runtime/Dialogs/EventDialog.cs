using System;
using JunkyardDogs.scripts.Runtime.Dialogs;
using PandeaGames;
using UnityEngine;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

public class EventDialog : Dialog<EventDialogViewModel>
{
    private JunkyardUser _user;
    private Tournament _tournament;

    protected override void Initialize()
    {
        base.Initialize();
        
        _viewModel.ModelData.TournamentReference.LoadAsync(OnTournamentLoaded, OnLoadError);
    }

    private void OnLoadError(LoadException loadException)
    {
        
    }

    private void OnTournamentLoaded()
    {
        Tournament tournament = _viewModel.ModelData.TournamentReference.Asset as Tournament;
        TournamentState state = null;
        
        _user.Tournaments.TryGetTournament(tournament, out state);

        if (state == null)
        {
            state = tournament.GenerateState();
        }

        Debug.Log("Display Tourny");
    }
}
