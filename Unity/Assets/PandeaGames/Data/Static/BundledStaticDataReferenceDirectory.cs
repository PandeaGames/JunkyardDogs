using System;
using System.Diagnostics;
using AssetBundles;
using Object = UnityEngine.Object;

namespace PandeaGames.Data.Static
{
    public class BundledStaticDataReferenceDirectory<TDataBase, TData, TReference, TDirectory> : StaticDataReferenceDirectory<TDataBase, TData, TReference, TDirectory>
        where TDataBase:Object
        where TData:TDataBase 
        where TReference:StaticDataReference<TDataBase, TData, TReference, TDirectory>, new()
        where TDirectory:StaticDataReferenceDirectory<TDataBase, TData, TReference, TDirectory>, new()
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
            {
                onLoadFailed(new LoadException(string.Format("Failed to load source data at path {0}", _path)));
                return;
            }

            TaskProvider.Instance.RunTask(request, () =>
            {
                Object requestResult = request.GetAsset<UnityEngine.Object>();
                IStaticDataDirectorySource<TData> source = requestResult as IStaticDataDirectorySource<TData>;

                if (source == null)
                {
                    onLoadFailed(new LoadException(string.Format("Failed to cast loaded data source at path '{0}' into source type {1} and data type {2}", 
                        _path,
                        typeof(IStaticDataDirectorySource),
                        typeof(TData))));
                }
                else
                {
                    onLoadSuccess(source);
                }
            });
        }
    }
}