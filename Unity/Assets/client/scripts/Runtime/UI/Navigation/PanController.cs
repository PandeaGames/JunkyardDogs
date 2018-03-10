using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanController : MonoBehaviour {

    [SerializeField]
    private RectTransform _target;

    [SerializeField]
    private RectTransform _container;

    [SerializeField]
    private bool zoom;

    [SerializeField]
    private ServiceManager _serviceManager;

    private InputService _inputService;
    private Vector2 _pointerPosition;
    private bool _isPanning;
    private Transform _targetTransform;

    // Use this for initialization
    void Start () {
        _inputService = _serviceManager.GetService<InputService>();

        _inputService.OnPointerMove += OnPointerMove;
        _inputService.OnPointerDown += OnPointerDown;
        _inputService.OnPointerUp += OnPointerUp;

        _targetTransform = _target.transform;
    }

    void OnDestroy()
    {
        _inputService.OnPointerMove -= OnPointerMove;
        _inputService.OnPointerDown -= OnPointerDown;
        _inputService.OnPointerUp -= OnPointerUp;

        _inputService = null;
    }

    private void OnPointerUp(Vector2 cameraPosition, Vector2 worldPosition, RaycastHit2D[] raycast = null)
    {
        _isPanning = false;
    }

    private void OnPointerDown(Vector2 cameraPosition, Vector2 worldPosition, RaycastHit2D[] raycast = null)
    {
        _isPanning = true;
        _pointerPosition = cameraPosition;
    }

    private void OnPointerMove(Vector2 cameraPosition, Vector2 worldPosition, RaycastHit2D[] raycast = null)
    {
        _targetTransform.Translate(cameraPosition - _pointerPosition);
        _pointerPosition = cameraPosition;

        Rect targetRect = _target.rect;
        Rect containerRect = _container.rect;

        float xDelta = targetRect.width / 2 - containerRect.width / 2;
        float yDelta = targetRect.height / 2 - containerRect.height / 2;

        float x = _target.anchoredPosition.x;
        float y = _target.anchoredPosition.y;

        if (xDelta < x)
            x = xDelta;
        else if(-xDelta > x)
            x = -xDelta;

        if (yDelta < y)
            y = yDelta;
        else if(-yDelta > y)
            y = -yDelta;

        _target.anchoredPosition = new Vector2(x, y);
    }
}