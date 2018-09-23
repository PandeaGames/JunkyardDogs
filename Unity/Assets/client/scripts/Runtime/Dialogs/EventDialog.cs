using System;
using UnityEngine;
using WeakReference = Data.WeakReference;

public class EventDialog : Dialog
{
    public class EventDialogConfig : Config
    {
        public WeakReference Tournament;
        public ServiceManager ServiceManager;
    }
    
    [NonSerialized]
    private EventDialogConfig _config;

    private JunkyardUserService _junkyardUserService;
    private JunkyardUser _user;
    private Tournament _tournament;

    public override void Setup(Config config, DialogResponseDelegate responseDelegate = null)
    {
        base.Setup(config, responseDelegate);
        _config = config as EventDialogConfig;
        _junkyardUserService = _config.ServiceManager.GetService<JunkyardUserService>();
        _user = _junkyardUserService.Load();
        _config.Tournament.LoadAsync<Tournament>(OnTournamentLoaded, () => { });
    }

    private void OnTournamentLoaded(Tournament tournament, WeakReference reference)
    {
        TournamentState state = null;
        
        _user.Tournaments.TryGetTournament(reference, out state);

        if (state == null)
        {
            state = tournament.GenerateState();
        }

        Debug.Log("Display Tourny");
    }
}
