using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[Serializable]
public abstract class Service : MonoBehaviour {

    public delegate void serviceStatusDelegate();

    public event serviceStatusDelegate OnServiceStart;
    public event serviceStatusDelegate OnServiceEnd;

    [SerializeField]
    private bool _isRunning;

    protected ServiceManager _manager;

    public bool IsRunning { get { return _isRunning; } }

    public virtual void StartService(ServiceManager serviceManager)
    {
        _isRunning = true;
        _manager = serviceManager;
        
        if (OnServiceStart != null)
        {
            OnServiceStart();
        }
    }

    public virtual void EndService(ServiceManager serviceManager)
    {
        _isRunning = false;
        _manager = null;

        if (OnServiceEnd != null)
        {
            OnServiceEnd();
        }
    }
}
