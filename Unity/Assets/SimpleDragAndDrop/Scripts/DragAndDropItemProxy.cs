using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class DragAndDropItemProxy : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public event Action<PointerEventData> OnBeginDragEvent;
    public event Action<PointerEventData> OnDragEvent;
    public event Action<PointerEventData> OnEndDragEvent;
    
    /// <summary>
    /// This item started to drag.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (OnBeginDragEvent != null)
            OnBeginDragEvent(eventData);
    }

    /// <summary>
    /// Every frame on this item drag.
    /// </summary>
    /// <param name="data"></param>
    public void OnDrag(PointerEventData data)
    {
        if (OnDragEvent != null)
            OnDragEvent(data);
    }

    /// <summary>
    /// This item is dropped.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (OnEndDragEvent != null)
            OnEndDragEvent(eventData);
    }
}
