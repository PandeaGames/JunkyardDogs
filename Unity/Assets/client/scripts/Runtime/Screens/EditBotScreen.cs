using UnityEngine;
using System.Collections;
using JunkyardDogs;
using PandeaGames;
using UnityEngine.UI;
using PandeaGames.Views.Screens;

public class EditBotScreen : ScreenView
{
    [SerializeField]
    private Button _exitButton;

    [SerializeField]
    private Button _editBehaviourButton;

    [SerializeField]
    private Button _dismantleButton;

    private GarageViewModel _viewModel;

    public override void Setup(WindowView window)
    {
        base.Setup(window);

        _viewModel = Game.Instance.GetViewModel<GarageViewModel>(0);
        _exitButton.onClick.AddListener(() => _viewModel.BlurBotBuilder());
        _editBehaviourButton.onClick.AddListener(_viewModel.OnEditBehaviourClicked);
        _dismantleButton.onClick.AddListener(_viewModel.DismantleSelected);
    }

    private void OnDestroy()
    {
        _exitButton.onClick.RemoveAllListeners();
        _editBehaviourButton.onClick.RemoveAllListeners();
        _dismantleButton.onClick.RemoveAllListeners();
    }

    private void OnEditBehaviourButtonClicked()
    {
        //var config = ScriptableObject.CreateInstance<EditBehaviourScreen.EditBehaviourConfig>();
        //config.Builder = _builderDisplay.BotBuilder;
        _window.LaunchScreen("EditBotBehaviour");
    }
}
