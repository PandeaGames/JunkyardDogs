using Data;
using UnityEngine;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

namespace PandeaGames
{
    [CreateAssetMenu(fileName = "SynchronousStaticData", menuName = "PandeaGaames/Config/SynchronousStaticData", order = 1)]
    public partial class SynchronousStaticData : ScriptableObject, ILoadableObject
    {
        partial void PopulatedLoadGroup(LoaderGroup loadGroup);
        
        public void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadFailed)
        {
            LoaderGroup loaderGroup = new LoaderGroup();
            PopulatedLoadGroup(loaderGroup);
            loaderGroup.LoadAsync(() =>
            {
                IsLoaded = true;
                onLoadSuccess();
            }, onLoadFailed);
        }

        public bool IsLoaded { get; private set; }
    }
}