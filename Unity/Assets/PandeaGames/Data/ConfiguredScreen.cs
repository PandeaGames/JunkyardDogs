using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class ConfiguredScreen : ScriptableObject
{
    [SerializeField]
    private string _sceneId;

    public string SceneId { get { return _sceneId; } }

    public ConfiguredScreen(string sceneId)
    {
        _sceneId = sceneId;
    }
}
