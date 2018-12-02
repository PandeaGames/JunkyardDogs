using AssetBundles;
using System;

namespace Data
{
    public class AssetLoader : ILoadableObject
    {
        private bool _isLoaded;
        private string _bundle;
        private string _path;
        private UnityEngine.Object _asset;
        public UnityEngine.Object Asset
        {
            get { return _asset; }
        }

        public bool IsLoaded()
        {
            return _isLoaded;
        }

        public AssetLoader(string bundle, string path)
        {
            _bundle = bundle;
            _path = path;
        }

        public void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadError)
        {
            try
            {
                // Load asset from assetBundle.
                string fileName = System.IO.Path.GetFileNameWithoutExtension(_path);
                AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync(_bundle, fileName, typeof(UnityEngine.Object));

                if (request == null)
                    return;

                TaskProvider.Instance.RunTask(request, () =>
                {
                    _asset = request.GetAsset<UnityEngine.Object>();
                    _isLoaded = true;

                    if (_asset is ILoadableObject)
                    {
                        (_asset as ILoadableObject).LoadAsync(onLoadSuccess, onLoadError);
                    }
                    else
                    {
                        onLoadSuccess();
                    }
                });
            }
            catch (Exception e)
            {
                onLoadError(new LoadException(e.ToString(), e));
            }
        }
    }
}