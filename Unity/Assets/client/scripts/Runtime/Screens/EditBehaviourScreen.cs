using UnityEngine;
using System.Collections;
using JunkyardDogs.Components;

public class EditBehaviourScreen : ScreenController
{
    public class EditBehaviourConfig : Config
    {
        public BotBuilder Builder;
    }

    [SerializeField]
    private ServiceManager _serviceManager;

    private EditBehaviourConfig _editBehaviourConfig;

    public override void Setup(WindowController window, Config config)
    {
        base.Setup(window, config);
        _editBehaviourConfig = config as EditBehaviourConfig;
    }

    private void OnEditBehaviourButtonClicked()
    {
        //_window.LaunchScreen("");
    }
}

