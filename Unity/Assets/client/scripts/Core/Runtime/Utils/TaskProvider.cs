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

    // Update is called once per frame
    void Update()
    {

    }

    public void RunTask( IEnumerator task, Action onComplete )
    {
        StartCoroutine(Run(task, onComplete));
    }

    private IEnumerator Run(IEnumerator task, Action onComplete)
    {
        yield return StartCoroutine(task);

        if (onComplete != null)
            onComplete();
    }
}
