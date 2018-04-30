using UnityEngine;
using System.Collections;

public class GameBootstrap : MonoBehaviour
{
    [SerializeField]
    private ServiceManager _serviceManager;

    [SerializeField]
    private WindowController _mainWindow;

    private JunkyardUserService _junkyardUserService;

    // Use this for initialization
    void Start()
    {
        _junkyardUserService = _serviceManager.GetService<JunkyardUserService>();

        ScreenController.Config config = ScriptableObject.CreateInstance<ScreenController.Config>();

        //_mainWindow.LaunchScreen("junkyard", config);
        _mainWindow.LaunchScreen("chooseCountry", config);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
