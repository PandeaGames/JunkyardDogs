using UnityEngine;
using System.Collections;
using PandeaGames.Runtime.Dialogs.ViewModels;
using TMPro;

public class OptionDisplay : MonoBehaviour, MessageDialog.IOptionDisplay
{
    [SerializeField]
    private TMP_Text _text;

    public void DisplayOption(MessageDialogViewModel.Option option)
    {
        _text.text = option.Title;
    }
}
