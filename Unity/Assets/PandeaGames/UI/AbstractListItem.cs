using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbstractListItem<TItemData> : MonoBehaviour, IListItem<TItemData>
{
    [SerializeField]
    private Button _button;
    
    private TItemData _data;
    public event Action<IListItem<TItemData>> OnSelect;

    private void Start()
    {
        _button.onClick.AddListener(OnClick);
    }
    
    public virtual void SetData(TItemData data)
    {
        _data = data;
    }

    public TItemData GetData()
    {
        return _data;
    }

    public void SetState(ListItemState state)
    {
    
    }

    private void OnClick()
    {
        OnSelect?.Invoke(this);
    }
}
