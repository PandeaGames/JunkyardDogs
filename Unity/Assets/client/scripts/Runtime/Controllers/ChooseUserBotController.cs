using UnityEngine;
using JunkyardDogs.Components;
using System;
using System.Collections.Generic;

public class ChooseUserBotController : MonoBehaviour, ChooseBotScreen.IChooseBotModel
{
    [SerializeField] 
    private BotRenderConfiguration _botRenderConfiguration;

    [SerializeField]
    private ServiceManager _serviceManager;
    
    [SerializeField]
    private PrefabFactory _botPrefabFactory;

    [SerializeField] 
    private LineupCameraAgent _cameraAgent;

    [SerializeField]
    private Transform _botContainer;

    [SerializeField] 
    private Vector3 _botLineupOffset;

    private List<BotRenderer> _renderedBots;
    private CameraService _cameraService;
    private JunkyardUserService _userService;
    private Action<Bot> _onChoose;
    private Action _onCancel;
    private Action _onError;
    private JunkyardUser _user;
    

    private void Start()
    {
        _cameraService = _serviceManager.GetService<CameraService>();
        _userService = _serviceManager.GetService<JunkyardUserService>();
        _renderedBots = new List<BotRenderer>();
    }
    
    public void ChooseBot(Action<Bot> onChoose, Action onCancel, Action onError)
    {
        _onChoose = onChoose;
        _onCancel = onCancel;
        _onError = onError;
        
        _user = _userService.Load();

        Competitor competitor = _user.Competitor;
        Inventory inventory = competitor.Inventory;

        int botsToLoad = inventory.Bots.Count;
        bool errorLoading = false;
        
        Action onBotLoadComplete = () =>
        {
            if (--botsToLoad <= 0)
            {
                if (errorLoading)
                {
                    onError();
                }
                else
                {
                    OnBotsLoaded(inventory.Bots);
                }
            }
        };
        
        Action onBotLoadError = () =>
        {
            botsToLoad--;
            errorLoading = true;
        };
        
        foreach (Bot bot in inventory.Bots)
        {
            bot.LoadAsync(onBotLoadComplete, onBotLoadError);
        }
        
        _cameraService.Focus(_cameraAgent);
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

        ChooseBotScreen.ChooseBotConfig config = ScriptableObject.CreateInstance<ChooseBotScreen.ChooseBotConfig>();
        config.Bots = bots;
        config.Model = this;
        
        WindowController.main.LaunchScreen("ChooseBotScreen", config);
    }
    
    public void Focus(Bot bot)
    {
        _cameraAgent.SetTarget(_renderedBots[_user.Competitor.Inventory.Bots.IndexOf(bot)].transform); 
    }
    
    public void Select(Bot bot)
    {
        WindowController.main.Back();
        _onChoose(bot);
    }
}
