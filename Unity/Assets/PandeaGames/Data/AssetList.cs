using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class AssetList<T> : ScriptableObject where T:UnityEngine.Object, ILoadableObject
{
    [SerializeField][WeakReference]
    private Data.WeakReferenceList<T> _list;

    public Data.WeakReferenceList<T> List
    {
        get { return _list; }
    }
}

