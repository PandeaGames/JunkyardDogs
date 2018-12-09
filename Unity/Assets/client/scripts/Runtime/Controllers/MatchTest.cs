using System.Collections;
using Data;
using JunkyardDogs;
using JunkyardDogs.Components;
using JunkyardDogs.Data;
using JunkyardDogs.Simulation;
using PandeaGames;
using UnityEngine;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

public class MatchTest : MonoBehaviour
{
    [SerializeField] 
    private MatchTestData _testData;

    [SerializeField]
    public bool _realTime;

    private JunkyardUserService _userService;
    private SimulationService _simulationService;
    private Engagement _engagement;
    private MatchTestViewModel _viewModel;
    private MatchTestViewController _vc;

    private void Start()
    {
        _viewModel = Game.Instance.GetViewModel<MatchTestViewModel>(0);
        _userService = Game.Instance.GetService<JunkyardUserService>();
        JunkyardUser user = _userService.User;

        _viewModel.TestData = _testData;
        _viewModel.RealTime = _realTime;

        _vc = new MatchTestViewController();
        _vc.ShowView();
    }

    private void Update()
    {
        _vc.Update();
    }
}
