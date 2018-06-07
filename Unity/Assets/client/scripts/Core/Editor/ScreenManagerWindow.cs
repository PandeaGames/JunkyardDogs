using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.SceneManagement;
using System;
using EditorSceneManager = UnityEditor.SceneManagement.EditorSceneManager;
    
public class LoadedScreen
{
    private ScreenController _screenController;
    private Scene _scene;
    private GameObject _duplicateScreen;
    private LoadedScreenCache _loadedScreenCache;
    public LoadedScreen(Scene scene, ScreenController screenController, LoadedScreenCache loadedScreenCache)
    {
        _scene = scene;
        _screenController = screenController;
        _loadedScreenCache = loadedScreenCache;
    }

    public LoadedScreen(LoadedScreenCache loadedScreenCache)
    {
        _loadedScreenCache = loadedScreenCache;

        _scene = SceneManager.GetSceneByPath(loadedScreenCache.scenePath);
        _duplicateScreen = GameObject.FindObjectOfType<ScreenController>().gameObject;

        if (_scene.isLoaded)
        {
            foreach (GameObject rootObject in _scene.GetRootGameObjects())
            {
                _screenController = rootObject.GetComponent<ScreenController>();

                if (_screenController != null)
                {
                    break;
                }
            }
        }
    }

    public GameObject GetDuplicateScreen()
    {
        if (!_duplicateScreen)
        {
            _duplicateScreen = GameObject.Instantiate(_screenController.gameObject);
        }

        _duplicateScreen.name = _screenController.gameObject.name;
        _duplicateScreen.SetActive(true);

        return _duplicateScreen;
    }

    public void Export()
    {
        GameObject screenToMove = GameObject.Instantiate(_duplicateScreen);
        screenToMove.name = _duplicateScreen.name;
        screenToMove.SetActive(false);

        if (_scene.isLoaded)
        {
            foreach (GameObject rootObject in _scene.GetRootGameObjects())
            {
                _screenController = rootObject.GetComponent<ScreenController>();

                if (_screenController != null)
                {
                    GameObject.DestroyImmediate(_screenController.gameObject);
                    break;
                }
            }
        }

        
        EditorSceneManager.MoveGameObjectToScene(screenToMove, _scene);
        EditorSceneManager.SaveScene(_scene);
    }

    public void Unload()
    {
        GameObject.DestroyImmediate(_duplicateScreen);

        LoadedScreenCache cache = GameObject.FindObjectOfType<LoadedScreenCache>();

        if(cache != null)
        {
            GameObject.DestroyImmediate(cache.gameObject);
        }

        SceneManager.UnloadSceneAsync(_scene);
    }
}

public class ScreenManagerWindow : EditorWindow
{
    private string[] _scenePathsCache;
    private string[] _scenePaths
    {
        get
        {
            if(_scenePathsCache == null)
            {
                _scenePathsCache = CoreEditorUtils.ReadScenePaths();
            }

            return _scenePathsCache;
        }
    }

    private ScreenController[] _screensCache;
    private Dictionary<Scene, ScreenController> _screensTable;
    private ScreenController[] _screens;
    private LoadedScreen _loadedScreen = null;
    private LoadedScreenCache _loadedScreenCache;

    [NonSerialized]
    private bool hasSearchedForCache = false;

    public void OnGUI()
    {
        if(_loadedScreenCache == null && !hasSearchedForCache)
        {
            _loadedScreenCache = GameObject.FindObjectOfType<LoadedScreenCache>();
            hasSearchedForCache = true;

            if (_loadedScreenCache != null)
            {
                _loadedScreen = new LoadedScreen(_loadedScreenCache);
            }
        }

        if(_loadedScreen == null)
        {
            OnSceneListGUI();
        }
        else
        {
            OnLoadedScreenGUI(_loadedScreen);
        }
    }

    private void OnSceneListGUI()
    {
        Scene activeScene = EditorSceneManager.GetActiveScene();

        foreach (string path in _scenePaths)
        {
            Scene scene = SceneManager.GetSceneByPath(path);

            bool isActiveScene = activeScene == scene;

            EditorGUI.BeginDisabledGroup(isActiveScene);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(CoreEditorUtils.GetSceneNameFromPath(path));

            if (GUILayout.Button("Import"))
                ImportScene(path);

            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }

    private void OnLoadedScreenGUI(LoadedScreen loadedScreen)
    {
        if (GUILayout.Button("Export"))
            loadedScreen.Export();

        if (GUILayout.Button("Unload"))
        {
            loadedScreen.Unload();
            _loadedScreen = null;
        }  
    }

    private void ImportScene(string path)
    {
        WindowController windowController = GameObject.FindObjectOfType<WindowController>();

        if(windowController == null)
        {
            Debug.LogError("WindowController not found in scene.");
            return;
        }

        Scene scene = EditorSceneManager.OpenScene(path, UnityEditor.SceneManagement.OpenSceneMode.Additive);
        Scene activeScene = EditorSceneManager.GetActiveScene();
        ScreenController screenController = null;
        //EditorSceneManager.MergeScenes(scene, activeScene);

        GameObject[] rootObjects = scene.GetRootGameObjects();

        foreach(GameObject rootObject in rootObjects)
        {
            screenController = rootObject.GetComponent<ScreenController>();

            if (screenController != null)
            {
                break;
            }
        }

        if (screenController == null)
        {
            Debug.LogError("ScreenController not found in loaded scene. ");
            SceneManager.UnloadSceneAsync(scene);
            return;
        }
        else
        {
            if(_loadedScreenCache == null)
            {
                GameObject cache = new GameObject("Cache");
                _loadedScreenCache = cache.AddComponent<LoadedScreenCache>();
            }

            _loadedScreen = new LoadedScreen(scene, screenController, _loadedScreenCache);
            GameObject duplicateScreen = _loadedScreen.GetDuplicateScreen();
            duplicateScreen.transform.SetParent(windowController.transform, false);

            RectTransform rectTransform = duplicateScreen.GetComponent<RectTransform>();

            if (rectTransform)
            {
                rectTransform.anchorMin = Vector2.zero;
                rectTransform.anchorMax = Vector2.one;
                rectTransform.offsetMax = Vector2.zero;
                rectTransform.offsetMin = Vector2.zero;

                rectTransform.hasChanged = false;
            }

            _loadedScreenCache.scenePath = scene.path;
            _loadedScreenCache._duplicateScreen = duplicateScreen;

            screenController.gameObject.SetActive(false);
        }
    }
}


