using PandeaGames;
using UnityEngine;
using PandeaGames.Views.Screens;
using UnityEngine.UI;

public class MapScreen : ScreenView
{
    [SerializeField]
    private Button _junkyardButton;

    private WorldMapViewModel _worldMapViewModel;

    // Use this for initialization
    void Start()
    {
        _worldMapViewModel = Game.Instance.GetViewModel<WorldMapViewModel>(0);
    }
}
