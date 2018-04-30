using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Data;
using System;
using WeakReference = Data.WeakReference;

[CreateAssetMenu]
public class NationList : ScriptableObject, IEnumerable<WeakReference>, ILoadableObject
{
    [SerializeField][WeakReference(typeof(Nationality))]
    private List<WeakReference> _nations;

    private int _loadedNations;
    private bool _isLoaded = false;

    public IEnumerator GetEnumerator()
    {
        return _nations.GetEnumerator();
    }

    IEnumerator<WeakReference> IEnumerable<WeakReference>.GetEnumerator()
    {
        return ((IEnumerable<WeakReference>)_nations).GetEnumerator();
    }

    bool ILoadableObject.IsLoaded()
    {
        return _isLoaded;
    }

    public void LoadAsync(Action onLoadSuccess, Action onLoadFailed)
    {
        if(_isLoaded)
        {
            return;
        }

        foreach(WeakReference nationReference in _nations)
        {
            nationReference.LoadAsync<Nationality>(
                (Nationality nationality, WeakReference reference) => OnNationLoaded(onLoadSuccess),
                null);
        }
    }

    private void OnNationLoaded(Action onLoadSuccess)
    {
        if (++_loadedNations >= _nations.Count)
        {
            _isLoaded = true;
            onLoadSuccess();
        }
    }
}