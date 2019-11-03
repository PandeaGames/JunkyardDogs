using System;

public interface IListView<TItemData>
{
    event Action<TItemData> OnItemSelected;
    void SetData(TItemData[] data);
    void SetSelected(TItemData data);
    void SetSelectedIndex(int index);
}
