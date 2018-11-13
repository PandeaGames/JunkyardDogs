using UnityEngine;
using System.Collections;
using System;

namespace PandeaGames.Views.Screens
{
    public class ScreenView : MonoBehaviour
    {
        public delegate void ScreenControllerDelegate(ScreenView view);

        public event ScreenControllerDelegate OnTransitionComplete;
        public event Action OnExit;
        public event Action OnBack;

        protected WindowView _window;
        protected RectTransform _rectTransform;

        public virtual void Setup(WindowView window)
        {
            _window = window;
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
            if (_rectTransform != null && _rectTransform.hasChanged)
            {
                _rectTransform.anchorMin = Vector2.zero;
                _rectTransform.anchorMax = Vector2.one;
                _rectTransform.sizeDelta = Vector2.zero;

                _rectTransform.hasChanged = false;
            }
        }

        public virtual void Transition(ScreenTransition transition)
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

        protected void Exit()
        {
            if (OnExit != null)
                OnExit();
        }

        protected void Back()
        {
            if (OnBack != null)
                OnBack();
        }
    }
}
