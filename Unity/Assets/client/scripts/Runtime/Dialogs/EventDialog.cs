using System;
using JunkyardDogs;
using JunkyardDogs.scripts.Runtime.Dialogs;
using PandeaGames;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using JunkyardDogs.Specifications;
using Component = JunkyardDogs.Components.Component;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

public class EventDialog : Dialog<EventDialogViewModel>
{
    [SerializeField]
    private RectTransform _tournamentViewContainer;

    [SerializeField]
    private GameObject _tournamentView;

    [SerializeField]
    private Button _playButton;
    
    [SerializeField]
    private Button _collectRewardsButton;

    [SerializeField]
    private TournamentTimerDisplay _timerDisplay;
    
    private JunkyardUser _user;
    private Tournament _tournament;
    private JunkyardUserViewModel _userViewModel;
    private TournamentState.TournamentStatus _status;
    private TournamentState _state;

    protected override void Initialize()
    {
        base.Initialize();
        _userViewModel = Game.Instance.GetViewModel<JunkyardUserViewModel>(0);
        _viewModel.TournamentReference.LoadAsync(OnTournamentLoaded, OnLoadError);

        _user = _userViewModel.UserData;
        
        _playButton.onClick.AddListener(() =>
        {
            _viewModel.PlayTournament(_status);
            Close();
        });
        
        _collectRewardsButton.onClick.AddListener(OnCollectRewards);
    }

    private void Update()
    {
        if (_state != null)
        {
            _playButton.interactable = TournamentStateUtils.IsRoundReady(_tournament, _state);
        }
    }

    private void OnLoadError(LoadException loadException)
    {
        
    }

    private void OnTournamentLoaded()
    {
        Tournament tournament = _viewModel.TournamentReference.Asset as Tournament;
        TournamentMetaState meta = null;
        TournamentState state = null;
        _tournament = tournament;
        
        _user.Tournaments.TryGetTournament(tournament, out state);
        _user.Tournaments.TryGetTournamentMeta(tournament, out meta);

        if (state == null)
        {
            state = tournament.GenerateState();
        }

        _state = state;
        _status = state.GetStatus();

        GameObject tournamentViewInstance = Instantiate(_tournamentView, _tournamentViewContainer, false);
        TournamentStateRenderer renderer = tournamentViewInstance.GetComponent<TournamentStateRenderer>();
        renderer.Render(state);

        _collectRewardsButton.gameObject.SetActive(state.IsComplete());
        _playButton.gameObject.SetActive(meta.CanPlay(tournament));
        _timerDisplay.Render(tournament, meta);
    }

    private void OnCollectRewards()
    {
        Tournament tournament = _viewModel.TournamentReference.Asset as Tournament;
        SpecificationCatalogue rewards = tournament.Rewards;
        List<Component> components = new List<Component>();
        _user.Cash += tournament.GoldReward;
        foreach (var product in rewards.Products)
        {
            Component component =
            ManufacturerUtils.BuildComponent(rewards.Manufacturer, product.Specification, product.Material);
            components.Add(component);
            _user.AddComponent(component);
            if (components.Count >= rewards.Products.Length)
            {
                _user.Tournaments.CompleteTournament(tournament.Guid);
                Game.Instance.GetService<JunkyardUserService>().Save();
                Close();
            }
        }
    }
}
