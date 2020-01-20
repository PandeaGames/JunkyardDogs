using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Disable import of materials if the file contains
// the @ sign marking it as an animation.
public class JunkyardAssetPostprocessor : AssetPostprocessor
{
    public Dictionary<string, string> folderAssetGroups = new Dictionary<string, string>()
    {
        {"Assets/AssetBundles/Data", "data"}
    };
    
    void OnPreprocessModel()
    {
        foreach (KeyValuePair<string, string> kvp in folderAssetGroups)
        {
            if (assetImporter.assetPath.Contains(kvp.Key))
            {
                assetImporter.assetBundleName = kvp.Value;
            }
        }
    }
}