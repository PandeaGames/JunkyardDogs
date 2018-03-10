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
            EditorGUILayout.LabelField(GetSceneNameFromPath(path));

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
        if(_scenePaths == null)
        {
            _scenePaths = ReadScenePaths();
        }
    }

    private string GetSceneNameFromPath(string path)
    {
        return path.Substring(path.LastIndexOf('/') + 1);
    }

    private static string[] ReadScenePaths()
    {
        List<string> temp = new List<string>();
        foreach (UnityEditor.EditorBuildSettingsScene S in UnityEditor.EditorBuildSettings.scenes)
        {
            if (S.enabled)
            {
                string path = S.path;
                temp.Add(path);
            }
        }
        return temp.ToArray();
    }

    private void ImportScene(string path)
    {
        EditorSceneManager.OpenScene(path, UnityEditor.SceneManagement.OpenSceneMode.Additive);
        //EditorSceneManager.LoadScene(path);
        Scene scene = SceneManager.GetSceneByPath(path);
        Scene activeScene = EditorSceneManager.GetActiveScene();
        EditorSceneManager.MergeScenes(scene, activeScene);
        //UnityEditor.SceneManagement.EditorSceneManager.LoadScene(scene.buildIndex);
        // SceneManager.LoadScene(scene.buildIndex);
    }

    private void OpenScene(string path)
    {
        EditorSceneManager.OpenScene(path);
       // SceneManager.SetActiveScene(scene);
    }
}