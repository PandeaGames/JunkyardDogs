using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractListView<TItemData, TListItemView> : MonoBehaviour, IListView<TItemData> where TListItemView : IListItem<TItemData>
{
    [SerializeField]
    private GameObject _listItemViewGO;
    
    [SerializeField]
    private Transform _itemViewContainer;  
    
    private TItemData[] _data;
    private TListItemView[] _listItemViews;
    private int _selectedIndex;
    public event Action<TItemData> OnItemSelected;

    private void Start()
    {
        _listItemViewGO.SetActive(false);
    }
    
    public void SetData(IEnumerable<TItemData> data)
    {
        List<TItemData> list = new List<TItemData>(data);
        SetData(list.ToArray());
    }
    public void SetData(TItemData[] data)
    {
        _data = data;
        InitializeListView(data);
    }

    private void InitializeListView(TItemData[] data)
    {
        _listItemViews = new TListItemView[data.Length];

        for (int i = 0; i < data.Length; i++)
        {
            TItemData itemData = data[i];
            GameObject listItemViewGO = Instantiate(_listItemViewGO, _itemViewContainer, worldPositionStays: false);
            listItemViewGO.SetActive(true);
            TListItemView listItemView = listItemViewGO.GetComponent<TListItemView>();
            listItemView.SetData(itemData);
            _listItemViews[i] = listItemView;
            listItemView.OnSelect += OnItemSelect;
        }
    }

    public void SetSelected(TItemData data)
    {
        
    }

    public void SetSelectedIndex(int index)
    {
       
    }

    private void OnItemSelect(IListItem<TItemData> listItem)
    {
        OnItemSelected?.Invoke(listItem.GetData());
    }
}
