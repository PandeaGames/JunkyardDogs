using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoreEditorUtils
{
    public static string[] ReadScenePaths()
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

    public static string GetSceneNameFromPath(string path)
    {
        return path.Substring(path.LastIndexOf('/') + 1);
    }
}