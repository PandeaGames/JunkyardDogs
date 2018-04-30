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
        return _keys.Contains(key);
    }
}

public abstract class ObjectFactory<T, K> : ScriptableObject where K: ObjectManifestEntry<T>
{
    [SerializeField]
    private List<K> _manifest;

    [SerializeField]
    private T _default;

    public T GetAsset(ScriptableObject obj)
    {
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