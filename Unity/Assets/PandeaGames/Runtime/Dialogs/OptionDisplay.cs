using System;
using UnityEngine;
using System.Collections;
using I2.Loc;
using PandeaGames.Runtime.Dialogs.ViewModels;
using TMPro;
using UnityEngine.UI;

public class OptionDisplay : MonoBehaviour, MessageDialog.IOptionDisplay
{
    public event Action<MessageDialogViewModel.Option> OptionSelected;
    
    [SerializeField]
    private TMP_Text _text;

    private MessageDialogViewModel.Option _option;

    public void DisplayOption(MessageDialogViewModel.Option option)
    {
        _text.text = LocalizationManager.GetTranslation(option.Title);
        _option = option;
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        OptionSelected?.Invoke(_option);
    }
}
