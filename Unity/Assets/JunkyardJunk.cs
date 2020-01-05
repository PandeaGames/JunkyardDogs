using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class JunkyardJunk : MonoBehaviour
{
    [SerializeField] 
    private Collider _collider;

    [SerializeField, OptionalField]
    private Renderer _renderer;
    
    public event Action<int, int, JunkyardJunk> OnClicked;
    public event Action<int, int, JunkyardJunk> OnPointerDown;
    private int x;
    private int y;
    
    public void Setup(int x, int y)
    {
        this.x = x;
        this.y = y;
        
        InputService.Instance.OnPointerClick += InstanceOnOnPointerClick;
        InputService.Instance.OnPointerDown += InstanceOnOnPointerDown;

        if(_renderer != null) _renderer.enabled = !JunkyardUtils.HideJunkyardMeshs;
    }

    public void SetAvailableForCollection(bool enabled)
    {
        _collider.enabled = enabled;
    }
    
    public void SetIsVisible(bool visible)
    {
        gameObject.active = visible;
    }

    private void InstanceOnOnPointerClick(Vector3 cameraposition, RaycastHit raycast)
    {
        if (raycast.collider != null && raycast.collider.gameObject == gameObject && OnClicked != null)
        {
            OnClicked(x, y, this);
        }
    }
    
    private void InstanceOnOnPointerDown(Vector3 cameraposition, RaycastHit raycast)
    {
        if (raycast.collider != null && raycast.collider.gameObject == gameObject && OnPointerDown != null)
        {
            OnPointerDown(x, y, this);
        }
    }

    private void OnDestroy()
    {
        InputService.Instance.OnPointerClick -= InstanceOnOnPointerClick;
        InputService.Instance.OnPointerDown -= InstanceOnOnPointerDown;
    }

    private void OnMouseUp()
    {
        /*if (OnClicked != null)
        {
            OnClicked(x, y);
            Destroy(this.gameObject);
        }*/
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
