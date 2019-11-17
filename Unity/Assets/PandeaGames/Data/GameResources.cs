using Data;
using UnityEditor;
using UnityEngine;

namespace PandeaGames.Data
{

    
    public partial class GameResources : ScriptableObjectSingleton<GameResources>, ILoadableObject
    {
#if UNITY_EDITOR
        public const string AssetPath = "Assets/Resources/GameResources.asset";
        
        [MenuItem("PandeaGames/Data/Generate Game Resources")]
        public static void CreateAsset()
        {
            GameResources gameResources = ScriptableObjectFactory.CreateInstance<GameResources>();
            AssetDatabase.CreateAsset(gameResources, AssetPath);
            EditorUtility.DisplayDialog("Asset Created", string.Format("Asset has been added at '{0}'", AssetPath),
                "OK");
        }
#endif
        
        partial void PopulatedLoadGroup(LoaderGroup loadGroup);
        
        public void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadFailed)
        {
            LoaderGroup loaderGroup = new LoaderGroup();
            PopulatedLoadGroup(loaderGroup);
            loaderGroup.LoadAsync(onLoadSuccess, onLoadFailed);
        }

        public bool IsLoaded { get; }
    }
}