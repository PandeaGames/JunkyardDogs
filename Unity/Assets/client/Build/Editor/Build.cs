using AssetBundles;
using UnityEditor;
using UnityEngine;

public class Build
    {
        [MenuItem("Assets/AssetBundles/Build Streaming AssetBundles")]
        static public void BuildAssetBundles ()
        {
            BuildScript.BuildAssetBundles(Application.streamingAssetsPath);
        }
    }
