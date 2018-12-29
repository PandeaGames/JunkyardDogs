using PandeaGames.Views.Screens;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JunkyardScreen : ScreenView {

    [SerializeField]
    private Button _returnButton;

    [SerializeField]
    private TMP_Text _returnButtonText;

    public override void Setup(WindowView window)
    {
        base.Setup(window);

        _returnButton.onClick.AddListener(OnReturnPressed);
        _returnButtonText.text = string.Format(I2.Loc.LocalizationManager.GetTranslation("UI.screens.return_to"), "SOME SCREEN");
    }

    private void OnReturnPressed()
    {
        Back();
    }
}