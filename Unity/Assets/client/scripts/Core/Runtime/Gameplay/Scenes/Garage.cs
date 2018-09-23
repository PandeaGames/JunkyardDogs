using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using JunkyardDogs.Components;
using UnityEngine.UI.Extensions;
using System.Collections.Generic;

public enum GarageStates
{
    Lineup,
    BotFocus
}

public class Garage : StateMachine<GarageStates>
{
    [SerializeField]
    private ServiceManager _serviceManager;

    [SerializeField]
    private WindowController _window;

    [SerializeField]
    private GameObject _botBuilderDisplayPrefab;

    [SerializeField]
    private Transform _listContent;

    [SerializeField]
    private SimpleFollowAgent _lineupCameraAgent;

    [SerializeField]
    private float _spacing;

    private JunkyardUserService _junkardUserService;
    private DialogService _dialogService;
    private CameraService _cameraService;
    private BotBuilderDisplay _selectedBuilder;
    private JunkyardUser _user;
    private List<BotBuilderDisplay> _builders;

    public List<BotBuilderDisplay> Builders { get { return _builders; } }

    protected override void Start()
    {
        _junkardUserService = _serviceManager.GetService<JunkyardUserService>();
        _cameraService = _serviceManager.GetService<CameraService>();
        _dialogService = _serviceManager.GetService<DialogService>();
        _builders = new List<BotBuilderDisplay>();

        _user = _junkardUserService.Load();

        foreach (Bot bot in _user.Competitor.Inventory.Bots)
        {
            AddBuilder(bot, _user.Competitor.Inventory);
        }

        base.Start();
    }

    public void DismantleSelected()
    {
        if (_selectedBuilder != null)
        {
            if (_currentState == GarageStates.BotFocus)
            {
                SetState(GarageStates.Lineup);
            }
            
            _selectedBuilder.BotBuilder.Dismantle();
            _builders.Remove(_selectedBuilder);
            GameObject.Destroy(_selectedBuilder.gameObject);
            if (_builders.Count > 0)
            {
                OnSelectBuilder(_builders[0]);
            }
            
            _junkardUserService.Save();
        }
    }

    protected override void LeaveState(GarageStates state)
    {
        switch(state)
        {
            case GarageStates.Lineup:
                break;
            case GarageStates.BotFocus:

                if(_selectedBuilder)
                {
                    _selectedBuilder.Blur();
                }
                
                break;
        }
    }

    protected override void EnterState(GarageStates state)
    {
        switch (state)
        {
            case GarageStates.Lineup:

                _cameraService.Focus(_lineupCameraAgent);
                GarageScreen.GarageScreenConfig config = ScriptableObject.CreateInstance<GarageScreen.GarageScreenConfig>();
                config.Garage = this;
                _window.LaunchScreen("garageScreen", config);
                break;

            case GarageStates.BotFocus:

                if(_selectedBuilder)
                {
                    _selectedBuilder.Focus();
                    var editConfig = ScriptableObject.CreateInstance<EditBotScreen.EditBotScreenConfig>();
                    editConfig.BuilderDisplay = _selectedBuilder;
                    editConfig.Garage = this;
                    _window.LaunchScreen("EditBot", editConfig);
                    _cameraService.Focus(_selectedBuilder.CameraAgent);
                }
                
                break;
        }
    }

    public void AddBuilder(Bot bot, Inventory inventory)
    {
        AddBuilder(new BotBuilder(bot, inventory));
    }

    public void AddBuilder(BotBuilder builder)
    {
        BotBuilderDisplay botBuilderDisplay = Instantiate(_botBuilderDisplayPrefab).GetComponent<BotBuilderDisplay>();
        botBuilderDisplay.transform.SetParent(_listContent, false);
        botBuilderDisplay.gameObject.SetActive(true);
        botBuilderDisplay.Setup(builder);
        botBuilderDisplay.transform.position = new Vector3(0, 0, _builders.Count * _spacing);
        _builders.Add(botBuilderDisplay);
        botBuilderDisplay.OnSelect += OnSelectBuilder;

        if(_currentState == GarageStates.Lineup)
        {
            _lineupCameraAgent.SetTarget(botBuilderDisplay.transform);
        }
    }

    public void GoToLineup()
    {
        SetState(GarageStates.Lineup);
    }

    private void OnSelectBuilder(BotBuilderDisplay botBuilderDisplay)
    {
        if(_currentState == GarageStates.Lineup)
        {
            if(!botBuilderDisplay.IsFocused)
            {
                if(_selectedBuilder == botBuilderDisplay)
                {
                    SetState(GarageStates.BotFocus);
                }
                else
                {
                    _lineupCameraAgent.SetTarget(botBuilderDisplay.transform);
                }
            }

            _selectedBuilder = botBuilderDisplay;
        }
    }
}
