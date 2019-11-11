
    using System;
    using JunkyardDogs;
    using PandeaGames;
    using UnityEngine;
public class TournamentTimerDisplay : MonoBehaviour
{
    [SerializeField] 
    private AbstractProgressDisplay _progressDisplay;
    
    private JunkyardUserViewModel _userViewModel;

    private TournamentMetaState _meta;
    private Tournament _tournament;

    private void Awake()
    {
        _userViewModel = Game.Instance.GetViewModel<JunkyardUserViewModel>(0);
    }
    
    public void Render(Tournament tournament)
    {
        TournamentMetaState state = null;
        
        _userViewModel.UserData.Tournaments.TryGetTournamentMeta(tournament, out state);

        if (state != null)
        {
            Render(tournament, state);
        }
    }

    public void Render(Tournament tournament, TournamentMetaState meta)
    {
        _tournament = tournament;
        _meta = meta;
    }

    private void Update()
    {
        if (_tournament != null && _meta != null)
        {
            float percentage = 0;
            
            if (_meta.TournamentState != null)
            {
                percentage = TournamentStateUtils.GetRoundReadyPercentage(_tournament, _meta.TournamentState);
            }
            else
            {
                percentage = TournamentMetaStateUtils.GetPercentageUntilSeasonBegin(_tournament, _meta);
            }
            _progressDisplay.SetProgress(percentage);
        }
    }
}
