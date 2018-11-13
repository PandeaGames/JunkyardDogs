using UnityEngine;
using System.Collections;
using PandeaGames;

public class PauseGame : MonoBehaviour
{
    private PauseService _pauseService;

    // Use this for initialization
    void Start()
    {
        _pauseService = Game.Instance.GetService<PauseService>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            _pauseService.Toggle();
    }
}
