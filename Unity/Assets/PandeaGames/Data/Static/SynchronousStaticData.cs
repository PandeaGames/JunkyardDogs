using Data;
using UnityEngine;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

namespace PandeaGames
{
    [CreateAssetMenu(fileName = "SynchronousStaticData", menuName = "PandeaGaames/Config/SynchronousStaticData", order = 1)]
    public partial class SynchronousStaticData : ScriptableObject, ILoadableObject
    {
        private LoaderGroup _loaderGroup = new LoaderGroup();
        partial void PopulatedLoadGroup(LoaderGroup loadGroup);
        
        public void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadFailed)
        {
            LoaderGroup loaderGroup = new LoaderGroup();
            loaderGroup.AppendProvider(_loaderGroup);
            loaderGroup.AppendProvider(_componentArtConfig);
            loaderGroup.AppendProvider(_projectileArtConfig);
            loaderGroup.AppendProvider(_projectileImpactConfig);
            loaderGroup.AppendProvider(_hitscanStreamArtConfig);
            loaderGroup.AppendProvider(_pulseArtConfig);
            loaderGroup.AppendProvider(_pulseImpactConfg);
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