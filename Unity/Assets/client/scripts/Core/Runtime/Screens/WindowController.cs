using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public enum Direction
{
    TO,
    FROM
}

[Serializable]
public class ScreenTransition
{
    [SerializeField]
    private string _sceneId;
    [SerializeField]
    private Direction _direction;
    [SerializeField]
    private ScreenController.Config _screenConfig;
    [SerializeField]
    private ScreenController.Result _result;

    public string SceneId { get { return _sceneId; } }
    public Direction Direction { get { return _direction; } }
    public ScreenController.Config ScreenConfig { get { return _screenConfig; } }
    public ScreenController.Result Result { get { return _result; } }

    public ScreenTransition(Direction direction)
    {
        _direction = direction;
    }

    public ScreenTransition(string sceneId, ScreenController.Config screenConfig, Direction direction)
    {
        _direction = direction;
        _screenConfig = screenConfig;
        _sceneId = sceneId;
    }

    public ScreenTransition(string sceneId, ScreenController.Config screenConfig, ScreenController.Result result, Direction direction):this(sceneId, screenConfig, direction)
    {
        _result = result;
    }

    public ScreenTransition(string sceneId, ScreenController.Config screenConfig) : this(sceneId, screenConfig, Direction.TO)
    {

    }

    public ScreenTransition(string sceneId, ScreenController.Config screenConfig, ScreenController.Result result) : this(sceneId, screenConfig, result, Direction.TO)
    {

    }
}


public class WindowController : MonoBehaviour
{
    public static WindowController main;
    
    public event Action OnClose;

    [SerializeField]
    private bool _overlapTransitions;
    
    [SerializeField]
    private bool _mainWindow;

    private ScreenController _activeScreen;

    private void Awake()
    {
        if(main == null && _mainWindow)
            main = this;
    }
    
    protected virtual void Start()
    {
        ScreenController[] screenControllers = GetComponentsInChildren<ScreenController>();

        ScreenController screenController = null;

        if(screenControllers.Length > 0)
        {
            screenController = screenControllers[0];
        }

        if(screenController != null)
        {
            screenController.Setup(this, ScriptableObject.CreateInstance<ScreenController.Config>());
        }
    }

    public virtual void LaunchScreen(ScreenTransition transition)
    {
        StartCoroutine(LoadSceneAsync(transition));
    }

    public virtual void LaunchScreen(string sceneId, ScreenController.Config screenConfig)
    {
        if (_activeScreen)
        {
            LaunchScreen(new ScreenTransition(sceneId, screenConfig, _activeScreen.GetResult()));
        }
        else
        {
            LaunchScreen(new ScreenTransition(sceneId, screenConfig));
        }
    }

    public void RemoveScreen(ScreenController screen, ScreenTransition transition)
    {
        _activeScreen.Transition(transition);
        _activeScreen.OnTransitionComplete += TransitionComplete;
        _activeScreen.OnExit -= Close;
        _activeScreen.OnBack -= Back;
    }

    public virtual void Back()
    {
        Close();
    }

    protected void Close()
    {
        if(_activeScreen)
        {
            RemoveScreen(_activeScreen, new ScreenTransition(Direction.FROM));
        }

        if (OnClose != null)
            OnClose();
    }

    private IEnumerator LoadSceneAsync(ScreenTransition transition)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(transition.SceneId, LoadSceneMode.Additive);
        
        //Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        
        Scene scene = SceneManager.GetSceneByName(transition.SceneId);
        ActivateScene(scene, transition);
    }

    private void ActivateScene(Scene scene, ScreenTransition transition)
    {
        ScreenController controller = null;

        foreach( GameObject gameObj in scene.GetRootGameObjects())
        {
            controller = gameObj.GetComponent<ScreenController>();

            if (controller != null)
                break;
        }

        if(controller == null)
        {
            Debug.LogError("ScreenController not found in scene '"+transition.SceneId+"'");
            return;
        }

        if (_activeScreen != null)
        {
            RemoveScreen(_activeScreen, transition);
        }

        _activeScreen = controller;
        _activeScreen.gameObject.SetActive(true);

        controller.transform.SetParent(transform, true);
        controller.OnExit += Close;
        controller.OnBack += Back;
        controller.Setup(this, transition.ScreenConfig);
        controller.Transition(transition);

        RectTransform rt = controller.GetComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.sizeDelta = Vector2.zero;

        StartCoroutine(UnloadScene(scene));
    }

    public void TransitionScreen(ScreenTransition transition)
    {
        
    }

    private void TransitionComplete(ScreenController controller)
    {
        controller.OnTransitionComplete -= TransitionComplete;
        Destroy(controller.gameObject);
    }

    private IEnumerator UnloadScene(Scene scene)
    {
        AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync(scene);

        //Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        //done unload of scene
    }
}
