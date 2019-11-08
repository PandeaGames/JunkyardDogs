using AssetBundles;
using UnityEditor;
using UnityEngine;

public class Build
{
    [MenuItem("Assets/AssetBundles/Build Streaming AssetBundles")]
    public static void BuildAssetBundles ()
    {
        Debug.LogFormat("Build Streaming Asset Bundles at '{0}'", Application.streamingAssetsPath);
        BuildScript.BuildAssetBundles(Application.streamingAssetsPath);
    }
}