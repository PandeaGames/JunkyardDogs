using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

[Serializable]
public abstract class ObjectManifestEntry<T> : IEnumerable
{
    [SerializeField]
    private T _value;
    public T Value { get { return _value; } }

    [SerializeField]
    private List<ScriptableObject> _keys;

    public IEnumerator GetEnumerator()
    {
        return _keys.GetEnumerator();
    }

    public bool ContainsKey(ScriptableObject key)
    {
        for (int i = 0; i < _keys.Count; i++)
        {
            if (_keys[i].name == key.name)
            {
                return true;
            }
        }

        return false;
    }
}

public abstract class ObjectFactory<T, K> : ScriptableObject where K: ObjectManifestEntry<T>
{
    [SerializeField]
    private List<K> _manifest;

    [SerializeField]
    private T _default;

    public bool HasAsset(ScriptableObject obj)
    {
        if (obj == null)
        {
            throw new NullReferenceException("Trying to retrieve assets with 'null' as input.");
        }

        T result;
        K manifestEntry = _manifest.Find((entry) => { return entry.ContainsKey(obj); });

        return manifestEntry != null;
    }

    public virtual T GetAsset(ScriptableObject obj)
    {
        if (obj == null)
        {
            throw new NullReferenceException("Trying to retrieve assets with 'null' as input.");
        }
        
        T result;
        K manifestEntry = _manifest.Find((entry) => 
        {
            return entry.ContainsKey(obj);
        });

        if (manifestEntry == null)
        {
            result = _default;
        }
        else
        {
            result = manifestEntry.Value;
        }

        return result;
    }
}