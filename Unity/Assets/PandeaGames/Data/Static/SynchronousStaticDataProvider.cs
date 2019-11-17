using AssetBundles;
using Data;
using PandeaGames.Data;
using PandeaGames.Data.Static;
using UnityEngine;

namespace PandeaGames
{
    public partial class SynchronousStaticDataProvider : IStaticDataProvider
    {
        protected SynchronousStaticData staticData { get; private set; }
        
        public TData GetData<TData>(int type, string id)
        {
            return GetData<TData>(type, id.GetHashCode());
        }
        
        public TData GetData<TData>(int type, int id)
        {
            return default(TData);
        }
        
        public void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadFailed)
        {
            staticData = GameResources.Instance.SynchronousStaticData;
            staticData.LoadAsync(onLoadSuccess, onLoadFailed);
        }

        public bool IsLoaded { get; private set; }
    }
}