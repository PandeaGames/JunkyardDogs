using UnityEngine;
using System.Collections;
using PandeaGames;
using PandeaGames.Views.Screens;

public class StartGame : MonoBehaviour
{
    [SerializeField]
    private ServiceManager _serviceManager;

    [SerializeField]
    private WindowView _mainWindow;

    private JunkyardUserService _junkyardUserService;
    private JunkyardUser _user;

    // Use this for initialization
    void Start()
    {
        _junkyardUserService = Game.Instance.GetService<JunkyardUserService>();
        _user = _junkyardUserService.Load();

        if (_user.Competitor.Nationality == null)
        {
            _mainWindow.LaunchScreen("chooseCountry");
        }
        else
        {
            _mainWindow.LaunchScreen("worldMap");
        }
    }
}
