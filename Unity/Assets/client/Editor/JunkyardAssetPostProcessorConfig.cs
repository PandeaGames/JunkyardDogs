
using System.IO;
using PandeaGames.Utils;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class JunkyardAssetPostProcessorConfig : ScriptableObject
{
    private const string RESOURCE_PATH_SOURCE = "Assets/Resources/{0}.asset";
    
    private static JunkyardAssetPostProcessorConfig instance;
    public static JunkyardAssetPostProcessorConfig Instance
    {
        get
        {
            if (instance == null)
            {
                string assetFullPath = string.Format(RESOURCE_PATH_SOURCE, "JunkyardAssetPostProcessorConfig");
                string assetPathWithoutExtension = Path.GetFileNameWithoutExtension(assetFullPath);
                instance = Resources.Load<JunkyardAssetPostProcessorConfig>(assetPathWithoutExtension);
            }
                
            return instance;
        }
    }
    
#if UNITY_EDITOR
        
    protected static void CreateAsset(string resourcePathWithoutExtension)
    {
        string assetPath = string.Format(RESOURCE_PATH_SOURCE, resourcePathWithoutExtension);
        JunkyardAssetPostProcessorConfig resource = CreateInstance<JunkyardAssetPostProcessorConfig>();
        AssetDatabase.CreateAsset(resource, assetPath);
        EditorUtility.DisplayDialog("Asset Created", string.Format("Asset has been added at '{0}'", assetPath),
            "OK");
    }

    [MenuItem("StaticData/JunkyardAssetPostProcessorConfig")]
    public static void CreateAsset()
    {
        CreateAsset("JunkyardAssetPostProcessorConfig");
    }
    
#endif
}
