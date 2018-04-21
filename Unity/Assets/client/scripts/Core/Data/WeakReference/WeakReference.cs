using UnityEngine;
using Polenter.Serialization;
using AssetBundles;
using System;
using System.Collections;

namespace Data
{ 
    [Serializable]
    public class WeakReference
    {
        [SerializeField]
        private string _guid;
        [SerializeField]
        private string _path;

        public string GUID { get { return _guid; } set { _guid = value; } }
        public string Path { get { return _path; } set { _path = value; } }

        private string _bundleCache;
        private ScriptableObject _cache;
        private bool _loaded;

        [ExcludeFromSerialization]
        public ScriptableObject Asset {
            get
            {
                if (_cache == null)
                {
                    _cache = Load();
                }

                return _cache;
            }
        }

        public WeakReference Reference
        {
            set
            {
                GUID = value.GUID;
                Path = value.Path;
            }
        }

        public ScriptableObject Load()
        {
            string error;
            string bundleName = AssetBundleUtils.GetBundleNameFromPath(Path);

            LoadedAssetBundle bundle = AssetBundleManager.GetLoadedAssetBundle(AssetBundleUtils.GetBundleNameFromPath(Path), out error);

            _cache = bundle.m_AssetBundle.LoadAsset<ScriptableObject>(Path);
            return _cache;
        }

        public void LoadAssync<T>( Action<T, WeakReference> onComplete ) where T:ScriptableObject
        {
            // Load asset from assetBundle.
            string bundleName = AssetBundleUtils.GetBundleNameFromPath(Path);
            string fileName = System.IO.Path.GetFileNameWithoutExtension(Path);
            AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync(bundleName, fileName, typeof(ScriptableObject));

            if (request == null)
                return;

            TaskProvider.Instance.RunTask(request, () =>
            {
                _cache = request.GetAsset<ScriptableObject>() as T;
                _loaded = true;

                if (onComplete != null)
                    onComplete(_cache as T, this);
            });
        }
    }
}