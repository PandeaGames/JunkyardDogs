using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using JunkyardDogs;
using JunkyardDogs.Components;
using JunkyardDogs.Data;
using JunkyardDogs.Simulation.Agent;
using PandeaGames;
using PandeaGames.Views.Screens;
using JunkyardDogs.Simulation.Behavior;
using SRF;
using CPU = JunkyardDogs.Specifications.CPU;
using ComponentCPU = JunkyardDogs.Components.CPU;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

public class EditBehaviourScreen : ScreenView
{   
    [SerializeField]
    private Transform _directiveListContainer;

    [SerializeField] 
    private Text _cpuName;
    
    [SerializeField]
    private Button _swapCpuButton;

    [SerializeField]
    private GameObject _directiveSlotView;
    
    [SerializeField] 
    private Button _doneButton;

    private EditBotBehaviourViewModel _viewModel;
    private JunkyardUserViewModel _userViewModel;
    private Dictionary<int, DirectiveView> _directiveViewTable;
    private Dictionary<int, int> _directiveViewInstanceToSlotIndexTable;

    public override void Setup(WindowView window)
    {
        base.Setup(window);

        _directiveViewTable = new Dictionary<int, DirectiveView>();
        _directiveViewInstanceToSlotIndexTable = new Dictionary<int, int>(); 
        
        _viewModel = Game.Instance.GetViewModel<EditBotBehaviourViewModel>(0);
        _userViewModel = Game.Instance.GetViewModel<JunkyardUserViewModel>(0);
        
        _doneButton.onClick.AddListener(_viewModel.OnDoneClicked);
        _swapCpuButton.onClick.AddListener(_viewModel.SwapCPU);
        
        _viewModel.OnSwapCPU += ViewModelOnSwapCpu;
        _viewModel.OnDirectiveChosen += ViewModelOnDirectiveChosen;

        _directiveSlotView.SetActive(false);
        _directiveSlotView.transform.parent = null;
        Render();
    }

    private void ViewModelOnDirectiveChosen(int index)
    {
        ComponentCPU componentCpu = _viewModel.Bot.CPU;
        _directiveViewTable[index].SetupComponent(componentCpu.GetDirective(index));
    }

    private void ViewModelOnSwapCpu()
    {
        Render();
    }

    private void Render()
    {
        _directiveListContainer.DestroyChildren();
        _directiveViewInstanceToSlotIndexTable.Clear();
        _directiveViewTable.Clear();
        
        
        if (_viewModel.Bot.CPU != null)
        {
            ComponentCPU componentCpu = _viewModel.Bot.CPU;
            _cpuName.text = _viewModel.Bot.CPU.Spec.name;

            for (int i = 0; i < componentCpu.Spec.DirectiveSlotCount; i++)
            {
                GameObject directiveSlot = Instantiate(_directiveSlotView, _directiveListContainer, worldPositionStays: false);
                directiveSlot.SetActive(true);
                DirectiveView view = directiveSlot.GetComponent<DirectiveView>();
                _directiveViewTable.Add(i, view);
                _directiveViewInstanceToSlotIndexTable.Add(view.gameObject.GetInstanceID(), i);
                view.SetupComponent(componentCpu.GetDirective(i));
                view.OnClick += ViewOnClick;
            }
        }
    }

    private void ViewOnClick(DirectiveView obj)
    {
        _viewModel.ChooseDirective(_directiveViewInstanceToSlotIndexTable[obj.gameObject.GetInstanceID()]);
    }

    private void OnDestroy()
    {
        _viewModel.OnSwapCPU -= ViewModelOnSwapCpu;
        _viewModel = null;
        Destroy(_directiveSlotView);
    }
}

