using AssetBundles;
using Data;
using PandeaGames.Data;
using PandeaGames.Data.Static;
using UnityEngine;

namespace PandeaGames
{
    public partial class SynchronousStaticDataProvider : AbstractStaticDataProvider<SynchronousStaticDataProvider>
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

        protected override void InternalLoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadFailed)
        {
            staticData = GameResources.Instance.SynchronousStaticData;
            staticData.LoadAsync(onLoadSuccess, onLoadFailed);
        }
    }
}