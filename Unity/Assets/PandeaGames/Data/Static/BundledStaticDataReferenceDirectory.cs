using System;
using System.Diagnostics;
using AssetBundles;
using UnityEditor;
using Object = UnityEngine.Object;

namespace PandeaGames.Data.Static
{
    public class BundledStaticDataReferenceDirectory<TDataBase, TData, TReference, TDirectory> : StaticDataReferenceDirectory<TDataBase, TData, TReference, TDirectory>
        where TDataBase:IStaticData
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
        
        #if UNITY_EDITOR
        protected override IStaticDataDirectorySource<TData> LoadSimulatedSource()
        {
            string[] guids = AssetDatabase.FindAssets(string.Format("{0}", _path));
            if (guids.Length > 0)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                return AssetDatabase.LoadAssetAtPath<Object>(path)as IStaticDataDirectorySource<TData>;
            }

            return null;
        }
        #endif
        
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