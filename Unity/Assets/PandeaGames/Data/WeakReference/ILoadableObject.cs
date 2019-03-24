using UnityEngine;
using System;

public class LoadException : Exception
{
    public LoadException(string message) : base(message)
    {
        
    }
    
    public LoadException(string message, Exception innerException) : base(message, innerException)
    {
        
    }
}

public delegate void LoadError(LoadException exception); 
public delegate void LoadSuccess(); 

public interface ILoadableObject
{
    void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadFailed);
    bool IsLoaded { get; }
}
