using System;
using JunkyardDogs.Data;
using PandeaGames;
using UnityEngine;
using UnityEngine.UI;

public class JunkyardMapButton : MonoBehaviour
{
    [SerializeField, JunkyardStaticDataReference]
    private JunkyardStaticDataReference _junkyard;

    [SerializeField] 
    private Button _button;
    
    private WorldMapViewModel _vm;

    private void Start()
    {
        _vm = Game.Instance.GetViewModel<WorldMapViewModel>(0);
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDestroy()
    {
        _vm = null;
        _button.onClick.RemoveListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        _vm.TapJunkyard(_junkyard);
    }
}
