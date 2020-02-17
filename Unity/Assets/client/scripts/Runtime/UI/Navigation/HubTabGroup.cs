using System;
using PandeaGames;
using PandeaGames.UI;

public class HubTabGroup : TabGroup
{
    private HubViewModel _vm;
    
    protected override void Start()
    {
        base.Start();
        _vm = Game.Instance.GetViewModel<HubViewModel>(0);
        _vm.OnEnterState += OnEnterHubState;
        SetSelectedIndex((int) _vm.CurrentState);
        OnIndexChanged += OnTabIndexChanged;
    }

    private void OnDestroy()
    {
        _vm.OnEnterState -= OnEnterHubState;
        OnIndexChanged -= OnTabIndexChanged;
    }
    
    private void OnTabIndexChanged(int index)
    {
        _vm.SetState((HubStates)Enum.ToObject(typeof(HubStates) , index));
    }

    private void OnEnterHubState(HubStates state)
    {
        SetSelectedIndex((int) state);
    }
}
