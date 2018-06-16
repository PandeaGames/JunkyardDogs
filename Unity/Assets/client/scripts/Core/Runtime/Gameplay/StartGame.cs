using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour
{
    [SerializeField]
    private ServiceManager _serviceManager;

    [SerializeField]
    private WindowController _mainWindow;

    private JunkyardUserService _junkyardUserService;
    private JunkyardUser _user;

    // Use this for initialization
    void Start()
    {
        _junkyardUserService = _serviceManager.GetService<JunkyardUserService>();
        _user = _junkyardUserService.Load();

        ScreenController.Config config = ScriptableObject.CreateInstance<ScreenController.Config>();

        if (_user.Competitor.Nationality == null)
        {
            _mainWindow.LaunchScreen("chooseCountry", config);
        }
        else
        {
            _mainWindow.LaunchScreen("worldMap", config);
        }
    }
}
