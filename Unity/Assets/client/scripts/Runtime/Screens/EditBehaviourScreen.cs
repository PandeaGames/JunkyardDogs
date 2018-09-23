using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Data;
using JunkyardDogs.Components;
using JunkyardDogs.Simulation.Agent;
using JunkyardDogs.Simulation.Behavior;

public class EditBehaviourScreen : ScreenController
{
    public class EditBehaviourConfig : Config
    {
        public BotBuilder Builder;
    }

    [SerializeField]
    private ServiceManager _serviceManager;

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

    [SerializeField][WeakReference(typeof(ActionList))] 
    private WeakReference _actionList;

    private JunkyardUserService _userService;
    private EditBehaviourConfig _editBehaviourConfig;
    private DialogService _dialogService;
    private InputService _inputService;
    private JunkyardUser _user;

    public override void Setup(WindowController window, Config config)
    {
        base.Setup(window, config);
        _editBehaviourConfig = config as EditBehaviourConfig;
        _userService = _serviceManager.GetService<JunkyardUserService>();
        _dialogService = _serviceManager.GetService<DialogService>();
        _user = _userService.Load();
        _doneButton.onClick.AddListener(Back);
        _newDirectiveButton.onClick.AddListener(OnNewDirectiveClick);
        
        if (_serviceManager.ServiceExists<InputService>())
        {
            _inputService = _serviceManager.GetService<InputService>();
        }
        
        SetupStates();
    }

    private void SetupStates()
    {
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>(); 
        
        if (_editBehaviourConfig.Builder.Bot.Agent.States.Count == 0)
        {
            _editBehaviourConfig.Builder.Bot.Agent.States.Add(new AgentState());
        }

        int stateNumber = 0;
        _editBehaviourConfig.Builder.Bot.Agent.States.ForEach((state) =>
        {
            options.Add(new Dropdown.OptionData(state.ToString() + stateNumber++));
        });
        
        _statesDropdown.AddOptions(options);

        _statesDropdown.onValueChanged.AddListener(OnStateSelectionChange);
        
        RenderAgentState(GetSelectedState());
    }

    private void OnStateSelectionChange(int value)
    {
        RenderAgentState(GetSelectedState());
    }
    
    public override void Transition(ScreenTransition transition)
    {
        base.Transition(transition);

        if (_inputService)
        {
            _inputService.enabled = transition.Direction == Direction.FROM;
        }
    }

    private void OnNewDirectiveClick()
    {
        //TODO: Select directive

        var config = ScriptableObject.CreateInstance<ChooseActionDialog.ChooseActionDialogConfig>();
        config.ActionList = _actionList;
        
        _dialogService.DisplayDialog<ChooseActionDialog>(config, SelectedNewAction);
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
                    RenderAgentState(GetSelectedState());
                });
            }
        });
    }

    private AgentState GetSelectedState()
    {
        List<AgentState> states = _editBehaviourConfig.Builder.Bot.Agent.States;
        int index = _statesDropdown.value;
        if (states.Count > index)
        {
            return states[index];
        }

        return null;
    }
    
    private void SelectedNewAction(Dialog.Response response)
    {
        var choice = response as ChooseActionDialog.ChooseActionDialogResponse;
        //TODO: Add action
        AgentState state = GetSelectedState();
        if (state != null && choice.Selection != null)
        {
            Directive directive = new Directive();
            directive.ActionWeakReference = choice.Selection;
            state.Directives.Add(directive);
            RenderAgentState(GetSelectedState());
            _userService.Save();
        }
    }

    private void RenderDirectives()
    {
        //_editBehaviourConfig.Builder.Bot.Agent.
    }

    private void OnSimpleDragAndDropEvent(DragAndDropCell.DropEventDescriptor desc)
    {
        if (desc.triggerType == DragAndDropCell.TriggerType.DropEventEnd)
        {
            AgentState state = GetSelectedState();
            state.Directives.Clear();

            DirectiveDisplay[] directiveDisplays = GetComponentsInChildren<DirectiveDisplay>();
        
            foreach (var directiveDisplay in directiveDisplays)
            {
                state.Directives.Add(directiveDisplay.Directive);
            }
        
            _userService.Save();
        }
    }
}

