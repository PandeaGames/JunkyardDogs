using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using EditorSceneManager = UnityEditor.SceneManagement.EditorSceneManager;

public class TemplateImporter : EditorWindow
{
    private string[] _scenePaths;

    [NonSerialized]
    private bool isLoaded = false;

    public void OnGUI()
    {
        Refresh();

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

            if (GUILayout.Button("Open"))
                OpenScene(path);

            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }

    private void Refresh()
    {
        if(!isLoaded)
        {
            _scenePaths = CoreEditorUtils.ReadScenePaths();
            isLoaded = true;
        }
    }

    private void ImportScene(string path)
    {
        EditorSceneManager.OpenScene(path, UnityEditor.SceneManagement.OpenSceneMode.Additive);
        Scene scene = SceneManager.GetSceneByPath(path);
        Scene activeScene = EditorSceneManager.GetActiveScene();
        EditorSceneManager.MergeScenes(scene, activeScene);
    }

    private void OpenScene(string path)
    {
        EditorSceneManager.OpenScene(path);
    }
}