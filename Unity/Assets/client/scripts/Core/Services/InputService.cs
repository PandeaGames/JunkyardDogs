using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputService : Service
{
    public delegate void OnMultiPointer(Vector3 cameraPosition, int index, RaycastHit raycast);
    public delegate void OnPointer(Vector3 cameraPosition, RaycastHit raycast);

    public event OnPointer OnPointerDown;
    public event OnPointer OnPointerUp;
    public event OnPointer OnPointerMove;

    public event OnMultiPointer OnMultiPointerDown;
    public event OnMultiPointer OnMultiPointerUp;
    public event OnMultiPointer OnMultiPointerMove;

    [SerializeField]
    private bool _touchEnabled;
    [SerializeField]
    private bool _providePonterRaycast;
    [SerializeField]
    private bool _useTriggersInRaycast;
    [SerializeField]
    private int _maxRaycastResults = 1;

    private bool _pointerDown;
    private int _touchCount;
    private Dictionary<int, Vector3> _touchDown;

    private Vector3 _gizmoPosition;
    private Vector3 _gizmoDirection;

    public override void StartService(ServiceManager serviceManager)
    {
        base.StartService(serviceManager);

        _touchDown = new Dictionary<int, Vector3>();
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(Camera.main.transform.position, Camera.main.transform.position + Camera.main.transform.forward * 20);
    }

    public override void EndService(ServiceManager serviceManager)
    {
        base.EndService(serviceManager);

        _touchDown.Clear();
        _touchDown = null;
    }

    protected virtual void Update()
    {
        HandlePointers();

        if(_touchEnabled)
            HandleTouches();
    }

    private void HandlePointers()
    {
        if (_pointerDown)
        {
            var pos = Input.mousePosition;
            pos.z = 10;
            Debug.Log("From Service Input.mousePosition ["+ pos + "] Camera.main.ScreenToWorldPoint(cameraPosition) ["+ Camera.main.ScreenToWorldPoint(pos) +"]");
            HandlePointerAction(Input.mousePosition, OnPointerMove);
        }

        if (Input.GetMouseButtonDown(0) && OnPointerDown!=null)
        {
           
            HandlePointerAction(Input.mousePosition, OnPointerDown);
            _pointerDown = true;
        }

        if (Input.GetMouseButtonUp(0) && _pointerDown)
        {
            HandlePointerAction(Input.mousePosition, OnPointerUp);
            _pointerDown = false;
        }
    }

    private void HandleTouches()
    {
        Touch touch;

        for (int i = Input.touchCount; i < _touchCount; i++)
        {
            Vector3 cameraPosition;
            _touchDown.TryGetValue(i, out cameraPosition);

            HandleMultiPointerAction(cameraPosition, i, OnMultiPointerUp, OnPointerUp);
            _touchDown.Remove(i);
        }

        for (int i = 0; i < Input.touchCount; i++)
        {
            touch = Input.touches[i];

            Vector3 cameraPosition = touch.position;

            Vector3 target = Camera.main.ScreenToWorldPoint(touch.position);

            if (!_touchDown.ContainsKey(i))
            {
                _touchDown.Add(i, cameraPosition);
                HandleMultiPointerAction(cameraPosition, i, OnMultiPointerDown, OnPointerDown);
            }

            HandleMultiPointerAction(cameraPosition, i, OnMultiPointerMove, OnPointerMove);
        }

        _touchCount = Input.touchCount;
    }

    private void HandlePointerAction(Vector3 cameraPosition, OnPointer eventToNotify)
    {
        cameraPosition.z = 10;
        //TODO: always the same value. needs z value. is it not valuable at all? 
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(cameraPosition);
        Ray ray = Camera.main.ScreenPointToRay(cameraPosition);

        RaycastHit results = default(RaycastHit);

        if (_providePonterRaycast)
        {
            // Physics.Raycast(worldPosition, Camera.main.transform.forward,out results);
            Physics.Raycast(ray, out results);
        }

        if(eventToNotify != null)
            eventToNotify(worldPosition, results);
    }

    private void HandleMultiPointerAction(Vector2 cameraPosition, int index, OnMultiPointer eventToNotify, OnPointer pointerEventToNotify)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(cameraPosition);

        RaycastHit results = default(RaycastHit);

        if (_providePonterRaycast)
        {
            Physics.Raycast(cameraPosition, Camera.main.transform.forward, out results);
        }

        if (eventToNotify != null)
            eventToNotify(cameraPosition, index, results);

        if(index == 0 && pointerEventToNotify != null)
            pointerEventToNotify(cameraPosition, results);
    }
}