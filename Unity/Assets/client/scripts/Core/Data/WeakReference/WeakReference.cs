using UnityEngine;
using Polenter.Serialization;
using AssetBundles;
using System;
using System.Collections;

[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
public class WeakReferenceAttribute : PropertyAttribute
{
    private Type _typeRestriction;
    public Type TypeRestriction
    {
        get
        {
            return _typeRestriction;
        }
    }

    public WeakReferenceAttribute()
    {
        _typeRestriction = typeof(ScriptableObject);
    }

    public WeakReferenceAttribute(Type typeRestriction)
    {
        _typeRestriction = typeRestriction;
    }
}

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

        public void LoadAsync<T>( Action<T, WeakReference> onComplete, Action onFail ) where T:ScriptableObject
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