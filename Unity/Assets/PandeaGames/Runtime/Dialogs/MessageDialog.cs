using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using PandeaGames.Runtime.Dialogs.ViewModels;
using PandeaGames.ViewModels;
using UnityEngine.UI;

public class MessageDialog<TViewModel> : Dialog<MessageDialogViewModel> where TViewModel:MessageDialogViewModel
{
    [SerializeField]
    private GameObject _button;
    [SerializeField]
    private Transform _buttonContainer;

    protected override void Initialize()
    {
        base.Initialize();
        _viewModel.OnOptionSelected += OnOptionSelected;
        DisplayOptions(_viewModel.options);
    }

    protected virtual void DisplayOptions(List<MessageDialogViewModel.Option> options)
    {
        foreach(MessageDialogViewModel.Option option in options)
        {
            GameObject button = Instantiate(_button);
            button.SetActive(true);
            button.transform.SetParent(_buttonContainer.transform, false);
            IOptionDisplay optionDisplay = button.GetComponent<IOptionDisplay>();
            optionDisplay.DisplayOption(option);

            Button buttonComponent = button.GetComponent<Button>();
            buttonComponent.onClick.AddListener(() => OnOptionSelected(option));
        }
    }

    protected void OnOptionSelected(MessageDialogViewModel.Option option)
    {
        _viewModel.SelectOption(option);
        Close();
    }

    public interface IOptionDisplay
    {
        void DisplayOption(MessageDialogViewModel.Option option);
    }
}

public class MessageDialog : MessageDialog<MessageDialogViewModel>
{
   
}

