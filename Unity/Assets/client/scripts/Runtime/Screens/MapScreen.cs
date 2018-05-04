﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MapScreen : ScreenController
{
    [SerializeField]
    private ServiceManager _serviceManager;

    [SerializeField]
    private Button _junkyardButton;

    // Use this for initialization
    void Start()
    {
        _junkyardButton.onClick.AddListener(OnJunkyardButtonPressed);
    }

    private void OnJunkyardButtonPressed()
    {
        JunkyardScreen.JunkyardConfig config = ScriptableObject.CreateInstance<JunkyardScreen.JunkyardConfig>();

        config.returnScreen = "mapScreen";

        _window.LaunchScreen("junkyard", config);
    }
}
