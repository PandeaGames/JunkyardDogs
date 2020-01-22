using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbstractListItem<TItemData> : MonoBehaviour, IListItem<TItemData>
{
    [SerializeField]
    private Button _button;
    
    [SerializeField]
    protected TMP_Text _title;
    
    private TItemData _data;
    public event Action<IListItem<TItemData>> OnSelect;

    private void Start()
    {
        if (_button != null)
            _button.onClick.AddListener(OnClick);
    }

    protected virtual string GetName(TItemData item)
    {
        return item.ToString();
    }
    
    public virtual void SetData(TItemData data)
    {
        _data = data;
        if (_title != null)
        {
            _title.text = GetName(data);
        }
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
