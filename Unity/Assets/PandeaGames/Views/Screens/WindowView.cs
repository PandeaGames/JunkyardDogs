using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PandeaGames.Views.Screens
{
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
    
        public string SceneId { get { return _sceneId; } }
        public Direction Direction { get { return _direction; } }
    
        public ScreenTransition(Direction direction)
        {
            _direction = direction;
        }
    
        public ScreenTransition(string sceneId, Direction direction)
        {
            _direction = direction;
            _sceneId = sceneId;
        }
    
        public ScreenTransition(string sceneId) : this(sceneId, Direction.TO)
        {
    
        }
    }
    
    
    public class WindowView : MonoBehaviour
    {
        public static WindowView main;
        
        public event Action OnClose;
    
        [SerializeField]
        private bool _overlapTransitions;
        
        [SerializeField]
        private bool _mainWindow;
    
        private ScreenView _activeScreen;
    
        private void Awake()
        {
            if(main == null && _mainWindow)
                main = this;
        }
        
        protected virtual void Start()
        {
            ScreenView[] screenViews = GetComponentsInChildren<ScreenView>();
    
            ScreenView screenView = null;
    
            if(screenViews.Length > 0)
            {
                screenView = screenViews[0];
            }
    
            if(screenView != null)
            {
                screenView.Setup(this);
            }
        }
    
        public virtual void LaunchScreen(ScreenTransition transition)
        {
            StartCoroutine(LoadSceneAsync(transition));
        }
    
        public virtual void LaunchScreen(string sceneId)
        {
            if (_activeScreen)
            {
                LaunchScreen(new ScreenTransition(sceneId));
            }
            else
            {
                LaunchScreen(new ScreenTransition(sceneId));
            }
        }
    
        public void RemoveScreen(ScreenView screen, ScreenTransition transition)
        {
            if (screen != null)
            {
                _activeScreen.Transition(transition);
                _activeScreen.OnTransitionComplete += TransitionComplete;
                _activeScreen.OnExit -= Close;
                _activeScreen.OnBack -= Back;
            }
        }
    
        public virtual void Back()
        {
            Close();
        }

        public void RemoveCurrentScreen()
        {
            RemoveScreen(_activeScreen, new ScreenTransition(Direction.FROM));
            _activeScreen = null;
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
            SceneManager.UnloadSceneAsync(scene);
        }
    
        private void ActivateScene(Scene scene, ScreenTransition transition)
        {
            ScreenView view = null;
    
            foreach( GameObject gameObj in scene.GetRootGameObjects())
            {
                view = gameObj.GetComponent<ScreenView>();
    
                if (view != null)
                    break;
            }
    
            if(view == null)
            {
                Debug.LogError("ScreenController not found in scene '"+transition.SceneId+"'");
                return;
            }
    
            if (_activeScreen != null)
            {
                RemoveScreen(_activeScreen, transition);
            }
    
            _activeScreen = view;
            _activeScreen.gameObject.SetActive(true);
    
            view.transform.SetParent(transform, true);
            view.OnExit += Close;
            view.OnBack += Back;
            view.Setup(this);
            view.Transition(transition);
    
            RectTransform rt = view.GetComponent<RectTransform>();
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.sizeDelta = Vector2.zero;
            rt.localScale = Vector2.one;
            rt.offsetMax = Vector2.zero;
            rt.offsetMin = Vector2.zero;
    
            StartCoroutine(UnloadScene(scene));
        }
    
        public void TransitionScreen(ScreenTransition transition)
        {
            
        }
    
        private void TransitionComplete(ScreenView view)
        {
            view.OnTransitionComplete -= TransitionComplete;
            Destroy(view.gameObject);
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
}