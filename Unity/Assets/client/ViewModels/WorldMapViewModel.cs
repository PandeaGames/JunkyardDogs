using System;
using JunkyardDogs.Data;
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

    public event Action<TournamentStaticDataReference> OnTournamentTapped;
    public event Action<TournamentState.TournamentStatus> OnPlayTournament;
    public event Action<JunkyardData> OnJunkyardTapped;
    public event Action<Nationality> OnTryAscend;

    private Data _data;

    public JunkyardUser User
    {
        get { return _data.User; }
    }
    
    public void SetData(Data data)
    {
        _data = data;
    }
    
    public void TapTournament(TournamentStaticDataReference tournament)
    {
        if (OnTournamentTapped != null)
        {
            OnTournamentTapped(tournament);
        }
    }
    
    public void TapJunkyard(JunkyardData junkyard)
    {
        if (OnJunkyardTapped != null)
        {
            OnJunkyardTapped(junkyard);
        }
    }
    public void PlayTournament(TournamentState.TournamentStatus status)
    {
        if (OnPlayTournament != null)
        {
            OnPlayTournament(status);
        }
    }

    public void TryAscend(Nationality nationality)
    {
        OnTryAscend?.Invoke(nationality);
    }
}