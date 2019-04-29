using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using JunkyardDogs;
using JunkyardDogs.Data;
using JunkyardDogs.Simulation.Agent;
using PandeaGames;
using PandeaGames.Views.Screens;
using JunkyardDogs.Simulation.Behavior;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

public class EditBehaviourScreen : ScreenView
{
    [SerializeField]
    private GameObject _directiveGameObject;
    
    [SerializeField]
    private Transform _directiveListContainer;

    [SerializeField] 
    private Button _newDirectiveButton;
    
    [SerializeField] 
    private Button _doneButton;
    
    [SerializeField] 
    private Dropdown _statesDropdown;

    private EditBotBehaviourViewModel _viewModel;

    public override void Setup(WindowView window)
    {
        base.Setup(window);

        _viewModel = Game.Instance.GetViewModel<EditBotBehaviourViewModel>(0);
        
        _doneButton.onClick.AddListener(_viewModel.OnDoneClicked);
        _newDirectiveButton.onClick.AddListener(_viewModel.OnSelectNewDirectiveClicked);

        SetupStates();
    }

    private void OnDestroy()
    {
        _viewModel.OnSelectState -= RenderAgentState;
        _viewModel.OnActionAdded -= OnActionAdded;

        _viewModel = null;
    }

    private void SetupStates()
    {
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>(); 

        int stateNumber = 0;
        _viewModel.Bot.Agent.States.ForEach((state) =>
        {
            options.Add(new Dropdown.OptionData((state.ToString() + stateNumber.ToString())));
            stateNumber++;
        });
        
        if (_viewModel.Bot.Agent.States.Count == 0)
        {
            _viewModel.Bot.Agent.States.Add(new AgentState());
        }
        
        _statesDropdown.AddOptions(options);

        _statesDropdown.onValueChanged.AddListener(OnStateSelectionChange);
        _viewModel.OnSelectState += RenderAgentState;
        _viewModel.OnActionAdded += OnActionAdded;
        _viewModel.SetSelectedState(0);        
    }

    private void OnActionAdded(BehaviorAction behaviorAction, ActionStaticDataReference reference)
    {
        RenderAgentState(_viewModel.SelectedState);
    }

    private void OnStateSelectionChange(int value)
    {
        _viewModel.SetSelectedState(value);
    }
    
    private void RenderAgentState(AgentState agentState)
    {
        for (int i = 0; i < _directiveListContainer.childCount; i++)
        {
            GameObject.Destroy(_directiveListContainer.GetChild(i).gameObject);
        }

        if (agentState == null)
            return;
        
        agentState.Directives.ForEach((directive) =>
        {
            GameObject displayObject = Instantiate(_directiveGameObject, _directiveListContainer, false);
            DirectiveDisplay display = displayObject.GetComponentInChildren<DirectiveDisplay>();
            displayObject.gameObject.SetActive(true);
            display.Render(directive);

            Button internalButton = displayObject.GetComponentInChildren<Button>();
            if (internalButton != null)
                
            {
                internalButton.onClick.AddListener(() =>
                {
                    agentState.Directives.Remove(directive);
                    RenderAgentState(_viewModel.SelectedState);
                });
            }
        });
    }

    private void OnSimpleDragAndDropEvent(DragAndDropCell.DropEventDescriptor desc)
    {
        if (desc.triggerType == DragAndDropCell.TriggerType.DropEventEnd)
        {
            AgentState state = _viewModel.SelectedState;
            state.Directives.Clear();

            DirectiveDisplay[] directiveDisplays = GetComponentsInChildren<DirectiveDisplay>();
        
            foreach (var directiveDisplay in directiveDisplays)
            {
                state.Directives.Add(directiveDisplay.ActionDirective);
            }
        }
    }
}

