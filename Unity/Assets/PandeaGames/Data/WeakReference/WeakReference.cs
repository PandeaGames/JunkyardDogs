using UnityEngine;
using Polenter.Serialization;
using AssetBundles;
using System;
using System.Collections;
using UnityEngine.UI;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PandeaGames.Data.WeakReferences
{
    public class WeakReferenceException : Exception
    {
        private static readonly string ExceptionMessage = "Failed to load asset at path '{0}'";
        private static readonly string TypeExceptionMessage = "Asset at path '{0}' does not match type '{1}' requested.";
        
        public WeakReferenceException(WeakReference weakReference) : base(string.Format(ExceptionMessage, weakReference.Path))
        {
            
        }
        
        public WeakReferenceException(WeakReference weakReference, Exception innerException) : base(string.Format(ExceptionMessage, weakReference.Path),innerException)
        {
            
        }
        
        public WeakReferenceException(Type type, WeakReference weakReference) : base(string.Format(TypeExceptionMessage, weakReference.Path, type.ToString()))
        {
            
        }
        
        public WeakReferenceException(Type type, WeakReference weakReference, Exception innerException) : base(string.Format(TypeExceptionMessage, weakReference.Path, type.ToString()),innerException)
        {
            
        }
    }
    }

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
        _typeRestriction = typeof(UnityEngine.Object);
    }

    public WeakReferenceAttribute(Type typeRestriction)
    {
        _typeRestriction = typeRestriction;
    }
}

namespace PandeaGames.Data.WeakReferences
{
    [Serializable]
    public class WeakReference : ILoadableObject
    {
        [SerializeField]
        private string _guid;
        [SerializeField]
        private string _path;

        public string GUID { get { return _guid; } set { _guid = value; } }
        public string Path { get { return _path; } set { _path = value; } }

        private string _bundleCache;
        private UnityEngine.Object _cache;
        public bool IsLoaded { get; private set; }

        [ExcludeFromSerialization]
        public UnityEngine.Object Asset {
            get
            {
#if UNITY_EDITOR
                if (_cache == null)
                {
                    _cache = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(Path);
                }
#endif       
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
        public void LoadAssetAsync<T>(Action<T, WeakReference> onComplete, Action<WeakReferenceException> onFail) where T : UnityEngine.Object
        {
            LoadAssetAsync((reference) =>
            {
                if (reference.Asset is T)
                {
                    onComplete(reference.Asset as T, this);
                }
                else
                {
                    onFail(new WeakReferenceException(typeof(T), this));
                }
                
            }, onFail);
        }

        public void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadError)
        {
            LoadAssetAsync(WeakReference => onLoadSuccess(), (e) => onLoadError(new LoadException("Problem loading asset", e)));
        }
        
        public void LoadAssetAsync( Action<WeakReference> onComplete, Action<WeakReferenceException> onFail )
        {
            if (string.IsNullOrEmpty(Path))
            {
                TaskProvider.Instance.DelayedAction(() =>
                {
                    IsLoaded = true;

                    if (onComplete != null)
                        onComplete(this);
                });
                
                return;
            }

            try
            {
                // Load asset from assetBundle.
                string bundleName = AssetBundleUtils.GetBundleNameFromPath(Path);
                string fileName = System.IO.Path.GetFileNameWithoutExtension(Path);
                AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync(bundleName, fileName, typeof(UnityEngine.Object));

                if (request == null)
                    return;

                TaskProvider.Instance.RunTask(request, () =>
                {
                    _cache = request.GetAsset<UnityEngine.Object>();

                    if (_cache is ILoadableObject)
                    {
                        (_cache as ILoadableObject).LoadAsync(() =>
                        {
                            IsLoaded = true;
                            onComplete?.Invoke(this);
                        },  e => onFail(new WeakReferenceException(this, e)));
                    }
                    else
                    {
                        IsLoaded = true;
                        onComplete?.Invoke(this);
                    }
                    
                    if(_cache is IWeakReferenceObject)
                        (_cache as IWeakReferenceObject).SetReferences(Path, GUID);
                });
            }
            catch (Exception e)
            {
                onFail(new WeakReferenceException(this, e));
            }
        }
    }
}