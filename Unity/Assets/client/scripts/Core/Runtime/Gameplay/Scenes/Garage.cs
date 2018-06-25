using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using JunkyardDogs.Components;
using UnityEngine.UI.Extensions;
using System.Collections.Generic;

public class Garage : MonoBehaviour
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
    private JunkyardUser _user;
    private List<BotBuilderDisplay> _builders;

    public List<BotBuilderDisplay> Builders { get { return _builders; } }

    public void Start()
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

        _cameraService.Focus(_lineupCameraAgent);

        GarageScreen.GarageScreenConfig config = ScriptableObject.CreateInstance<GarageScreen.GarageScreenConfig>();
        config.Garage = this;
        _window.LaunchScreen("garageScreen", config);
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
        _lineupCameraAgent.SetTarget(botBuilderDisplay.transform);
        botBuilderDisplay.Setup(builder);
        botBuilderDisplay.transform.position = new Vector3(0, 0, _builders.Count * _spacing);
        _builders.Add(botBuilderDisplay);
        botBuilderDisplay.OnSelect += OnSelectBuilder;
    }

    private void OnSelectBuilder(BotBuilderDisplay botBuilderDisplay)
    {
        if(!botBuilderDisplay.IsFocused)
        {
            if(_lineupCameraAgent.Target == botBuilderDisplay.transform)
            {
                botBuilderDisplay.Focus();
                var config = ScriptableObject.CreateInstance<EditBotScreen.EditBotScreenConfig>();
                config.BuilderDisplay = botBuilderDisplay;
                _window.LaunchScreen("EditBot", config);
                _cameraService.Focus(botBuilderDisplay.CameraAgent);
            }
            else
            {
                _lineupCameraAgent.SetTarget(botBuilderDisplay.transform);
            }
        }
        else
        {

        }
    }
}
