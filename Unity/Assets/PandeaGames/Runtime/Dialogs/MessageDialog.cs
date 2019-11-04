using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using I2.Loc;
using PandeaGames.Runtime.Dialogs.ViewModels;
using PandeaGames.ViewModels;
using TMPro;
using UnityEngine.UI;

public class MessageDialog<TViewModel> : Dialog<MessageDialogViewModel> where TViewModel:MessageDialogViewModel
{
    [SerializeField]
    private GameObject _button;
    [SerializeField]
    private Transform _buttonContainer;

    [SerializeField] private TMP_Text _messageText;

    protected override void Initialize()
    {
        base.Initialize();
        DisplayOptions(_viewModel.options);
        _messageText.text = LocalizationManager.GetTranslation(_viewModel.Msg);
    }

    protected virtual void DisplayOptions(List<MessageDialogViewModel.Option> options)
    {
        foreach(MessageDialogViewModel.Option option in options)
        {
            GameObject button = Instantiate(_button, _buttonContainer.transform, false);
            button.SetActive(true);
            IOptionDisplay optionDisplay = button.GetComponent<IOptionDisplay>();
            optionDisplay.DisplayOption(option);
            optionDisplay.OptionSelected += OnOptionSelected;
        }
    }

    protected void OnOptionSelected(MessageDialogViewModel.Option option)
    {
        _viewModel.SelectOption(option);
        Close();
    }

    public interface IOptionDisplay
    {
        event Action<MessageDialogViewModel.Option> OptionSelected;
        void DisplayOption(MessageDialogViewModel.Option option);
    }
}

public class MessageDialog : MessageDialog<MessageDialogViewModel>
{
   
}

