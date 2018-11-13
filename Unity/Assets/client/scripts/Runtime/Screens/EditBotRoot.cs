using UnityEngine;
using System.Collections;
using JunkyardDogs.Components;
using PandeaGames.Views.Screens;

public class EditBotRoot : ScreenView
{
    [SerializeField]
    private ServiceManager _serviceManager;

    //private EditBotRootConfig _editBotScreenConfig;

    public override void Setup(WindowView window)
    {
        base.Setup(window);

        //_editBotScreenConfig = config as EditBotRootConfig;
    }

    private void OnEditBehaviourButtonClicked()
    {
        //_window.LaunchScreen("");
    }
}

