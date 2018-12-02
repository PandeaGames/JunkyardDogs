using PandeaGames.Data.WeakReferences;
using UnityEngine;

public abstract class AssetList<T> : ScriptableObject where T:UnityEngine.Object, ILoadableObject
{
    [SerializeField][WeakReference]
    private WeakReferenceList<T> _list;

    public WeakReferenceList<T> List
    {
        get { return _list; }
    }
}

