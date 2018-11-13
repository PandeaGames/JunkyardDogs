using PandeaGames.Views.Screens;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JunkyardScreen : ScreenView {

    /*public class JunkyardConfig : ScreenView.Config
    {
        public string returnScreen;
    }*/

   // private JunkyardConfig _junkyardConfig;

    [SerializeField]
    private Button _returnButton;

    [SerializeField]
    private TMP_Text _returnButtonText;

    public override void Setup(WindowView window)
    {
        base.Setup(window);
       /* _junkyardConfig = config as JunkyardConfig;

        if(_junkyardConfig)
        {
            _returnButton.onClick.AddListener(OnReturnPressed);
            _returnButtonText.text = string.Format(I2.Loc.LocalizationManager.GetTranslation("UI.screens.return_to"), _junkyardConfig.returnScreen);
        }*/
    }

    private void OnReturnPressed()
    {
        Back();
    }
}