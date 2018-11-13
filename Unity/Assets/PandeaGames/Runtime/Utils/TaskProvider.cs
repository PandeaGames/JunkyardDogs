using UnityEngine;
using System.Collections;
using System;

public class TaskProvider : MonoBehaviour
{
    public delegate IEnumerator Task();
    public static TaskProvider Instance;

    // Use this for initialization
    void Awake()
    {
        Instance = this;
    }

    public void RunTask( IEnumerator task, Action onComplete )
    {
        StartCoroutine(Run(task, onComplete));
    }
    
    public void RunTask( IEnumerator task )
    {
        StartCoroutine(Run(task, () => { }));
    }
    
    public void DelayedAction(Action onComplete )
    {
        StartCoroutine(Run(NullObjectsLoaded(), onComplete));
    }

    private IEnumerator Run(IEnumerator task, Action onComplete)
    {
        yield return StartCoroutine(task);

        if (onComplete != null)
            onComplete();
    }
    
    private IEnumerator NullObjectsLoaded()
    {
        yield return 0;
    }
}
