using System.IO;
using UnityEditor;
using UnityEngine;

namespace PandeaGames.Utils
{
    public abstract class ResourceSingleton<TResource> : ScriptableObject where TResource:ResourceSingleton<TResource>
    {
        private const string RESOURCE_PATH_SOURCE = "Assets/Resources/{0}.asset";
        
#if UNITY_EDITOR
        
        protected static void CreateAsset(string resourcePathWithoutExtension)
        {
            string assetPath = string.Format(RESOURCE_PATH_SOURCE, resourcePathWithoutExtension);
            ResourceSingleton<TResource> resource = CreateInstance<ResourceSingleton<TResource>>();
            AssetDatabase.CreateAsset(resource, assetPath);
            EditorUtility.DisplayDialog("Asset Created", string.Format("Asset has been added at '{0}'", assetPath),
                "OK");
        }
#endif
        private static TResource instance;

        public static TResource Instance
        {
            get
            {
                if (instance == null)
                {
                    string assetFullPath = string.Format(RESOURCE_PATH_SOURCE, "");
                    string assetPathWithoutExtension = Path.GetFileNameWithoutExtension(assetFullPath);
                    instance = Resources.Load<TResource>(assetPathWithoutExtension);
                }
                
                return instance;
            }
        }
    }
}