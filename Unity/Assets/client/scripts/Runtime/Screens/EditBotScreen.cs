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
    }

    [SerializeField]
    private ServiceManager _serviceManager;

    [SerializeField]
    private Button _exitButton;

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

        _exitButton.onClick.AddListener(OnExitClicked);
    }

    private void OnExitClicked()
    {
        //_exitButton
    }
}
