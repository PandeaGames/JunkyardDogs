using System;
using UnityEngine;

[CreateAssetMenu]
public class PostProcessorConfig : ScriptableObject
{
    public const string STATIC_DATA_PATH = "Assets/Editor/PostProcessorConfig.asset";
    
    [Serializable]
    public struct PathBundleSetters
    {
        public string path;
        public string bundle;
    }

    [SerializeField]
    public PathBundleSetters[] setters;
}
