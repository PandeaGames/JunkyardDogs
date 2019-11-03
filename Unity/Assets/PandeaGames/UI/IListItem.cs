using System;

public enum ListItemState
{
    Normal, 
    Selected
}

public interface IListItem<TItemData>
{
    event Action<IListItem<TItemData>> OnSelect;
    void SetData(TItemData data);
    TItemData GetData();
    void SetState(ListItemState state);
}
