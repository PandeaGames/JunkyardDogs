using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using JunkyardDogs.Components;
using UnityEngine.UI.Extensions;

public class EditBotScreen : ScreenController
{
    public class EditBotScreenConfig : Config
    {
        public BotBuilderDisplay BuilderDisplay;
        public Garage Garage;
    }

    [SerializeField]
    private ServiceManager _serviceManager;

    [SerializeField]
    private Button _exitButton;

    [SerializeField]
    private Button _editBehaviourButton;

    private JunkyardUserService _junkardUserService;
    private DialogService _dialogService;
    private EditBotScreenConfig _editBotScreenConfig;
    private BotBuilderDisplay _builderDisplay;

    public override void Setup(WindowController window, Config config)
    {
        base.Setup(window, config);

        _editBotScreenConfig = config as EditBotScreenConfig;
        _builderDisplay = _editBotScreenConfig.BuilderDisplay;
        _junkardUserService = _serviceManager.GetService<JunkyardUserService>();
        _dialogService = _serviceManager.GetService<DialogService>();
        _exitButton.onClick.AddListener(() => _editBotScreenConfig.Garage.GoToLineup());
        _editBehaviourButton.onClick.AddListener(OnEditBehaviourButtonClicked);
    }

    private void OnEditBehaviourButtonClicked()
    {
        var config = ScriptableObject.CreateInstance<EditBehaviourScreen.EditBehaviourConfig>();
        config.Builder = _builderDisplay.BotBuilder;
        _window.LaunchScreen("EditBotBehaviour", config);
    }
}
