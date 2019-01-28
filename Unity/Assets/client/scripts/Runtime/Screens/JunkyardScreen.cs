using PandeaGames.Views.Screens;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JunkyardScreen : ScreenView {

    [SerializeField]
    private TMP_Text _returnButtonText;

    private void OnReturnPressed()
    {
        Back();
    }
}