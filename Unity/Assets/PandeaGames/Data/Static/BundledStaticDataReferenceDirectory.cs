using System;
using AssetBundles;
using Object = UnityEngine.Object;

namespace PandeaGames.Data.Static
{
    public class BundledStaticDataReferenceDirectory<TData, TReference, TDirectory> : StaticDataReferenceDirectory<TData, TReference, TDirectory>
        where TData:Object
        where TReference:StaticDataReference<TData, TReference, TDirectory>, new()
        where TDirectory:StaticDataReferenceDirectory<TData, TReference, TDirectory>, new()
    {
        private string _bundleName;
        private string _path;
        
        public BundledStaticDataReferenceDirectory(string bundleName, string path)
        {
            _bundleName = bundleName;
            _path = path;
        }
        
        protected override void LoadSourceDataAsync(Action<IStaticDataDirectorySource<TData>> onLoadSuccess, LoadError onLoadFailed)
        {
            AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync(_bundleName, _path, typeof(UnityEngine.Object));

            if (request == null)
                return;

            TaskProvider.Instance.RunTask(request, () =>
            {
                Object requestResult = request.GetAsset<UnityEngine.Object>();
                IStaticDataDirectorySource<TData> source = requestResult as IStaticDataDirectorySource<TData>;
                onLoadSuccess(source);
            });
        }
    }
}