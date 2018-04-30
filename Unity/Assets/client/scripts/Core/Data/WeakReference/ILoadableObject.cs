using UnityEngine;
using System;

public interface ILoadableObject
{
    void LoadAsync(Action onLoadSuccess, Action onLoadFailed);
    bool IsLoaded();
}
