using UnityEngine;
using System.Collections;
using JunkyardDogs;
using UnityEngine.UI;
using JunkyardDogs.Components;
using JunkyardDogs.scripts.Runtime.Dialogs;
using PandeaGames;
using PandeaGames.Views.Screens;
using JunkyardDogs.scripts.Runtime.Dialogs;

public class GarageScreen : ScreenView
{
    [SerializeField]
    private Button _newBotButton;
    
    [SerializeField]
    private Button _dismantleButton;

    private GarageViewModel _viewModel;

    public override void Setup(WindowView window)
    {
        base.Setup(window);

        _viewModel = Game.Instance.GetViewModel<GarageViewModel>(0);

        _newBotButton.onClick.AddListener(_viewModel.OnNewBotClicked);
        _dismantleButton.onClick.AddListener(_viewModel.DismantleSelected);
    }

    private void OnDestroy()
    {
        _newBotButton.onClick.RemoveAllListeners();
        _dismantleButton.onClick.RemoveAllListeners();
    }
}
