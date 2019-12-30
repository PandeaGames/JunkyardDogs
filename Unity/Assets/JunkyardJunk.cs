using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkyardJunk : MonoBehaviour
{
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

        Debug.LogFormat("JunkyardJunk.Setup [x:{0}, y:{1}]", x, y);
    }

    private void InstanceOnOnPointerClick(Vector3 cameraposition, RaycastHit raycast)
    {
        Debug.LogFormat("JunkyardJunk.InstanceOnOnPointerClick [raycast.collider:{0}, raycast.collider.gameObject == gameObject:{1}]", raycast.collider == null ? "NULL":raycast.collider.gameObject.name, raycast.collider == null ? "NULL":(raycast.collider.gameObject == gameObject).ToString());
        if (raycast.collider != null && raycast.collider.gameObject == gameObject && OnClicked != null)
        {
            Debug.LogFormat("JunkyardJunk.OnClicked [gameObject:{0}]", gameObject.name);
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
