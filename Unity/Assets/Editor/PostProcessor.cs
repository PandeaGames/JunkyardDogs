using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class PostProcessor : JunkyardAssetPostprocessor
{
    [Serializable]
    public struct PathBundleSetters
    {
        public string path;
        public string bundle;
    }

    [SerializeField]
    public static PathBundleSetters[] setters;
    
    public static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        PostProcessorConfig config = AssetDatabase.LoadAssetAtPath<PostProcessorConfig>(PostProcessorConfig.STATIC_DATA_PATH);
        
        foreach (string str in importedAssets)
        {
            foreach (PostProcessorConfig.PathBundleSetters bundleSetter in config.setters)
            {
                if (str.Contains(bundleSetter.path))
                {
                    AssetImporter.GetAtPath(str).SetAssetBundleNameAndVariant(bundleSetter.bundle, "");
                    break;
                }
            }
        }
    }
}