using UnityEngine;
using System.Collections;
using JunkyardDogs.Components;

public class EditBotRoot : ScreenController
{
    public class EditBotRootConfig : Config
    {
        public BotBuilder BuilderDisplay;
    }

    [SerializeField]
    private ServiceManager _serviceManager;

    private EditBotRootConfig _editBotScreenConfig;

    public override void Setup(WindowController window, Config config)
    {
        base.Setup(window, config);

        _editBotScreenConfig = config as EditBotRootConfig;
    }

    private void OnEditBehaviourButtonClicked()
    {
        //_window.LaunchScreen("");
    }
}

