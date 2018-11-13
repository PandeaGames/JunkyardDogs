using UnityEngine;
using System.Collections;
using TMPro;

public class OptionDisplay : MonoBehaviour, MessageDialog.IOptionDisplay
{
    [SerializeField]
    private TMP_Text _text;

    public void DisplayOption(MessageDialog.Option option)
    {
        _text.text = option.Title;
    }
}
