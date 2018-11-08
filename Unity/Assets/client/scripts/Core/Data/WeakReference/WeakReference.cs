using UnityEngine;
using Polenter.Serialization;
using AssetBundles;
using System;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

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
        private UnityEngine.Object _cache;
        private bool _loaded;

        [ExcludeFromSerialization]
        public UnityEngine.Object Asset {
            get
            {
                return _cache;
            }
#if UNITY_EDITOR
            set
            {
                _cache = value;
                Path = AssetDatabase.GetAssetPath(value);
                GUID = AssetDatabase.AssetPathToGUID(Path);
            }
#endif
        }

        public WeakReference Reference
        {
            set
            {
                GUID = value.GUID;
                Path = value.Path;
            }
        }

        #if UNITY_EDITOR
        /*public T Load<T>() where T: UnityEngine.Object
        {
            if (_cache == null && !string.IsNullOrEmpty(Path))
            {
                _cache = AssetDatabase.LoadAssetAtPath<ScriptableObject>(Path);
            }

            return _cache as T;
        }  */        
#endif

    public void LoadAsync<T>( Action<T, WeakReference> onComplete, Action onFail ) where T:UnityEngine.Object
        {

            if (string.IsNullOrEmpty(Path))
            {
                TaskProvider.Instance.DelayedAction(() =>
                {
                    _loaded = true;

                    if (onComplete != null)
                        onComplete(null, this);
                });
                
                return;
            }

            // Load asset from assetBundle.
            string bundleName = AssetBundleUtils.GetBundleNameFromPath(Path);
            string fileName = System.IO.Path.GetFileNameWithoutExtension(Path);
            AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync(bundleName, fileName, typeof(T));

            if (request == null)
                return;

            TaskProvider.Instance.RunTask(request, () =>
            {
                _cache = request.GetAsset<T>();
                _loaded = true;
                
                if(_cache is IWeakReferenceObject)
                    (_cache as IWeakReferenceObject).SetReferences(Path, GUID);

                if (onComplete != null)
                    onComplete(_cache as T, this);
            });
        }
    }
}