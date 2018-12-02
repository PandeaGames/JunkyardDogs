using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Data;
using System;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

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

    public void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadFailed)
    {
        if(_isLoaded)
        {
            return;
        }

        Loader loader = new Loader();
        loader.AppendProvider(_nations);
        loader.LoadAsync(onLoadSuccess, onLoadFailed);
    }
}