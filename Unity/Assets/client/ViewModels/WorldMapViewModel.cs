﻿using System;
using PandeaGames.ViewModels;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

public class WorldMapViewModel : AbstractViewModel
{
    public class Data
    {
        public readonly JunkyardUser User;
        
        public Data(
            JunkyardUser user)
        {
            User = user;
        }
    }

    public event Action<WeakReference> OnTournamentTapped;
    public event Action<TournamentState.TournamentStatus> OnPlayTournament;
    public event Action OnJunkyardTapped;

    private Data _data;

    public JunkyardUser User
    {
        get { return _data.User; }
    }
    
    public void SetData(Data data)
    {
        _data = data;
    }
    
    public void TapTournament(WeakReference tournament)
    {
        if (OnTournamentTapped != null)
        {
            OnTournamentTapped(tournament);
        }
    }
    
    public void TapJunkyard()
    {
        if (OnJunkyardTapped != null)
        {
            OnJunkyardTapped();
        }
    }
    public void PlayTournament(TournamentState.TournamentStatus status)
    {
        if (OnPlayTournament != null)
        {
            OnPlayTournament(status);
        }
    }
}