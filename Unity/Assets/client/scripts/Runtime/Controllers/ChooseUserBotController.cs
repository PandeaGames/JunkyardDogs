using UnityEngine;
using JunkyardDogs.Components;
using System;
using System.Collections.Generic;
using JunkyardDogs;
using PandeaGames;
using PandeaGames.Views.Screens;

public class ChooseUserBotController : MonoBehaviour
{
    [SerializeField] 
    private BotRenderConfiguration _botRenderConfiguration;
    
    [SerializeField]
    private PrefabFactory _botPrefabFactory;

    [SerializeField] 
    private LineupCameraAgent _cameraAgent;

    [SerializeField]
    private Transform _botContainer;

    [SerializeField] 
    private Vector3 _botLineupOffset;

    private List<BotRenderer> _renderedBots;
    private CameraViewModel _cameraViewModel;
    private JunkyardUserService _userService;
    private Action<Bot> _onChoose;
    private JunkyardUserViewModel _userViewModel;
    private ChooseBotFromInventoryViewModel _viewModel;

    private void Start()
    {
        _viewModel = Game.Instance.GetViewModel<ChooseBotFromInventoryViewModel>(0);
        _cameraViewModel = Game.Instance.GetViewModel<CameraViewModel>(0);
        _userViewModel = Game.Instance.GetViewModel<JunkyardUserViewModel>(0);
        _renderedBots = new List<BotRenderer>();
        _viewModel.OnBotFocus += Focus;

        ChooseBot();
    }

    private void OnDestroy()
    {
        _viewModel.OnBotFocus -= Focus;
    }
    
    public void ChooseBot()
    {
        Competitor competitor = _userViewModel.UserData.Competitor;
        Inventory inventory = competitor.Inventory;

        int botsToLoad = inventory.Bots.Count;
        bool errorLoading = false;
        
        LoadSuccess onBotLoadComplete = () =>
        {
            if (--botsToLoad <= 0)
            {
                if (errorLoading)
                {
                    //onError();
                }
                else
                {
                    OnBotsLoaded(inventory.Bots);
                }
            }
        };
        
        LoadError onBotLoadError = (e) =>
        {
            botsToLoad--;
            errorLoading = true;
        };
        
        foreach (Bot bot in inventory.Bots)
        {
            bot.LoadAsync(onBotLoadComplete, onBotLoadError);
        }
        
        _cameraViewModel.Focus(_cameraAgent);
    }

    private void OnBotsLoaded(List<Bot> bots)
    {
        for (int i = 0; i < bots.Count; i++)
        {
            Bot bot = bots[i];
            GameObject botObject = _botPrefabFactory.InstantiateAsset(bot.Chassis.Specification, _botContainer, false);
            BotRenderer renderer = botObject.AddComponent<BotRenderer>();

            botObject.transform.position = botObject.transform.position + _botLineupOffset * i;
            
            renderer.Render(bot, _botRenderConfiguration);
            _renderedBots.Add(renderer);
        }

        if (_renderedBots.Count > 0)
        {
            _cameraAgent.SetTarget(_renderedBots[0].transform);
        }
    }
    
    public void Focus(Bot bot)
    {
        _cameraAgent.SetTarget(_renderedBots[_userViewModel.UserData.Competitor.Inventory.Bots.IndexOf(bot)].transform); 
    }
}
