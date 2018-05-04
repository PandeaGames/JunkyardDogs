using UnityEngine;
using System.Collections;
using System;

public class ScreenController : MonoBehaviour
{
    [Serializable]
    public class Config : ScriptableObject
    {

    }

    public class Result : ScriptableObject
    {

    }

    public delegate void ScreenControllerDelegate(ScreenController controller);

    public event ScreenControllerDelegate OnTransitionComplete;

    protected WindowController _window;
    protected Config _config;
    protected RectTransform _rectTransform;

    public virtual void Setup(WindowController window, Config config)
    {
        _window = window;
        _config = config;
    }

    public virtual void Start()
    {
        _rectTransform = GetComponent<RectTransform>();

        if (_rectTransform)
        {
            _rectTransform.anchorMin = Vector2.zero;
            _rectTransform.anchorMax = Vector2.one;
            _rectTransform.offsetMax = Vector2.zero;
            _rectTransform.offsetMin = Vector2.zero;

            _rectTransform.hasChanged = false;
        }
    }

    public void Update()
    {
        if(_rectTransform != null && _rectTransform.hasChanged)
        {
            _rectTransform.anchorMin = Vector2.zero;
            _rectTransform.anchorMax = Vector2.one;
            _rectTransform.sizeDelta = Vector2.zero;

            _rectTransform.hasChanged = false;
        }
    }

    public void Transition(ScreenTransition transition)
    {
        StartCoroutine(DelayedTransitionComplete());
    }

    private IEnumerator DelayedTransitionComplete()
    {
        yield return null;
        
        TransitionComplete();
    }

    protected void TransitionComplete()
    {
        if (OnTransitionComplete != null)
            OnTransitionComplete(this);
    }

    public Result GetResult()
    {
        return ScriptableObject.CreateInstance<Result>();
    }
}
