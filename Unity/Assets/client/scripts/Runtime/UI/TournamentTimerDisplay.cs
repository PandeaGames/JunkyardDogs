
    using System;
    using JunkyardDogs;
    using PandeaGames;
    using UnityEngine;
public class TournamentTimerDisplay : MonoBehaviour
{
    [SerializeField]
    private RectTransform _fill;

    [SerializeField]
    private RectTransform _container;
    
    private JunkyardUserViewModel _userViewModel;

    private TournamentState _state;
    private Tournament _tournament;
    

    private void Awake()
    {
        _userViewModel = Game.Instance.GetViewModel<JunkyardUserViewModel>(0);
    }
    
    public void Render(Tournament tournament)
    {
        TournamentState state = null;
        
        _userViewModel.UserData.Tournaments.TryGetTournament(tournament, out state);

        if (state != null)
        {
            Render(tournament, state);
        }
    }

    public void Render(Tournament tournament, TournamentState state)
    {
        _tournament = tournament;
        _state = state;
    }

    private void Update()
    {
        if (_tournament != null && _state != null)
        {
            float percentage = TournamentStateUtils.GetRoundReadyPercentage(_tournament, _state);
            
           _fill.anchorMax = new Vector2(0, 1);
            _fill.anchorMin = new Vector2(0, 0);
            
            _fill.offsetMax = new Vector2(_container.rect.width * percentage, 0);
            _fill.offsetMin = new Vector2(0,0);

            _fill.pivot = new Vector2(0, 0);
        }
    }
}
