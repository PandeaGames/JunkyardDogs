using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JunkyardScreen : ScreenController {

    public class JunkyardConfig : ScreenController.Config
    {
        public string returnScreen;
    }

    private JunkyardConfig _junkyardConfig;

    [SerializeField]
    private Button _returnButton;

    [SerializeField]
    private TMP_Text _returnButtonText;

    public override void Setup(WindowController window, Config config)
    {
        base.Setup(window, config);
        _junkyardConfig = config as JunkyardConfig;

        if(_junkyardConfig)
        {
            _returnButton.onClick.AddListener(OnReturnPressed);
            _returnButtonText.text = string.Format(I2.Loc.LocalizationManager.GetTranslation("UI.screens.return_to"), _junkyardConfig.returnScreen);
        }
    }

    private void OnReturnPressed()
    {

    }
}