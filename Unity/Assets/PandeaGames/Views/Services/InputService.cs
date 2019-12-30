using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using PandeaGames;
using PandeaGames.Data;
using PandeaGames.Data.Static;
using UnityEngine.EventSystems;

public class InputService : MonoBehaviourSingleton<InputService>
{
    public delegate void OnMultiPointer(Vector3 cameraPosition, int index, RaycastHit raycast);
    public delegate void OnPointer(Vector3 cameraPosition, RaycastHit raycast);

    public event OnPointer OnPointerDown;
    public event OnPointer OnPointerUp;
    public event OnPointer OnPointerMove;
    public event OnPointer OnPointerClick;

    public event OnMultiPointer OnMultiPointerDown;
    public event OnMultiPointer OnMultiPointerUp;
    public event OnMultiPointer OnMultiPointerMove;

    [SerializeField] 
    private bool _enabled = true;
    [SerializeField]
    private bool _touchEnabled;
    [SerializeField]
    private bool _providePonterRaycast;
    [SerializeField]
    private bool _useTriggersInRaycast;
    [SerializeField]
    private int _maxRaycastResults = 1;

    private Vector3 _clickDownPosition;
    private DateTime _pointerDownTimestamp;
    private bool _pointerDown;
    private int _touchCount;
    private Dictionary<int, Vector3> _touchDown;

    //TODO:Place in some sort of configuration scriptable object
    private int _clickTimeSpan = 300;
    private float _clickDistance = 40;

    private Ray _ray;

    public bool Enabled
    {
        get
        {
            return _enabled;
        }
        set { _enabled = value; }
    }

    private void Start()
    {
        _touchDown = new Dictionary<int, Vector3>();

        InputConfig config = Game.Instance.GetStaticDataPovider<PandeaGameDataProvider>().PandeaGameConfigurationData.InputConfig;

        _touchEnabled = config.TouchEnabled;
        _providePonterRaycast = config.ProvidePonterRaycast;
        _useTriggersInRaycast = config.UseTriggersInRaycast;
        _maxRaycastResults = config.MaxRaycastResults;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(_ray.origin, _ray.direction * 100);
    }

    private void OnDestroy()
    {
        _touchDown.Clear();
        _touchDown = null;
    }

    protected virtual void Update()
    {
        if (! _enabled)
        {
            return;
        }
       
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
            HandlePointerAction(Input.mousePosition, OnPointerMove);
        }
        
        if (Input.GetMouseButtonDown(0) && !_pointerDown)
        {
            HandlePointerAction(Input.mousePosition, OnPointerDown);
            _pointerDown = true;
            
            _pointerDownTimestamp = DateTime.UtcNow;
            _clickDownPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0) && _pointerDown)
        {
            
            HandlePointerAction(Input.mousePosition, OnPointerUp);
            _pointerDown = false;
            TimeSpan timeFromDown = DateTime.UtcNow - _pointerDownTimestamp;
            float clickDistance = Vector3.Distance(_clickDownPosition, Input.mousePosition);
            bool validClickTime = timeFromDown.Milliseconds < _clickTimeSpan;
            bool validClickDistance = clickDistance < _clickDistance;
            if (validClickTime && validClickDistance)
            {
                HandlePointerAction(Input.mousePosition, OnPointerClick);
            }
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
            else
            {
                HandleMultiPointerAction(cameraPosition, i, OnMultiPointerMove, OnPointerMove);
            }
        }

        _touchCount = Input.touchCount;
    }

    private void HandlePointerAction(Vector3 cameraPosition, OnPointer eventToNotify)
    {
        cameraPosition.z = 10;
        //TODO: always the same value. needs z value. is it not valuable at all? 
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(cameraPosition);
        Ray ray = Camera.main.ScreenPointToRay(cameraPosition);

        _ray = ray;

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