﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PandeaGames.Views.Screens;

public class EditBotScreen : ScreenView
{
    /*public class EditBotScreenConfig : Config
    {
        public BotBuilderDisplay BuilderDisplay;
        public Garage Garage;
    }*/

    [SerializeField]
    private ServiceManager _serviceManager;

    [SerializeField]
    private Button _exitButton;

    [SerializeField]
    private Button _editBehaviourButton;

    [SerializeField]
    private Button _dismantleButton;

    private JunkyardUserService _junkardUserService;
    private DialogService _dialogService;
    //private EditBotScreenConfig _editBotScreenConfig;
    private BotBuilderDisplay _builderDisplay;

    public override void Setup(WindowView window)
    {
        base.Setup(window);

        //_editBotScreenConfig = config as EditBotScreenConfig;
       /// _builderDisplay = _editBotScreenConfig.BuilderDisplay;
       /* _junkardUserService = _serviceManager.GetService<JunkyardUserService>();
        _dialogService = _serviceManager.GetService<DialogService>();
        _exitButton.onClick.AddListener(() => _editBotScreenConfig.Garage.GoToLineup());
        _editBehaviourButton.onClick.AddListener(OnEditBehaviourButtonClicked);
        _dismantleButton.onClick.AddListener(_editBotScreenConfig.Garage.DismantleSelected);*/
    }

    private void OnEditBehaviourButtonClicked()
    {
        //var config = ScriptableObject.CreateInstance<EditBehaviourScreen.EditBehaviourConfig>();
        //config.Builder = _builderDisplay.BotBuilder;
        _window.LaunchScreen("EditBotBehaviour");
    }
}
