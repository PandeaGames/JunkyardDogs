using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using JunkyardDogs.Components;
using UnityEngine.UI.Extensions;
using System.Collections.Generic;
using JunkyardDogs;
using PandeaGames;
using PandeaGames.Views;
using PandeaGames.Views.Screens;

public class Garage : MonoBehaviour
{
    [SerializeField]
    private GameObject _botBuilderDisplayPrefab;

    [SerializeField]
    private Transform _listContent;

    [SerializeField]
    private SimpleFollowAgent _lineupCameraAgent;

    [SerializeField]
    private float _spacing;

    private CameraViewModel _cameraViewModel;
    private BotBuilderDisplay _selectedBuilder;
    private List<BotBuilderDisplay> _builders;
    private GarageViewModel _viewModel;

    public List<BotBuilderDisplay> Builders { get { return _builders; } }

    private void Start()
    {
        _builders = new List<BotBuilderDisplay>();
        
        _viewModel = Game.Instance.GetViewModel<GarageViewModel>(0);
        _cameraViewModel = Game.Instance.GetViewModel<CameraViewModel>(0);

        _viewModel.OnBuilderAdded += AddBuilder;
        _viewModel.OnBuilderDismantled += RemoveBuilder;
        _viewModel.OnBuilderFocus += OnBuilderFocus;
        _viewModel.OnBuilderBlur += OnBuilderBlur;
        _viewModel.OnBuilderSelected += OnBuilderSelected;

        foreach (var builder in _viewModel.Builders)
        {
            AddBuilder(builder);
        }
        
        _viewModel.SelectBuilder(_viewModel.Builders[0]);
        _cameraViewModel.Focus(_lineupCameraAgent);
    }

    private void OnBuilderSelected(BotBuilder builder)
    {
        BotBuilderDisplay botBuilderDisplay = _builders.Find((display) => display.BotBuilder == builder);
        _lineupCameraAgent.SetTarget(botBuilderDisplay.transform);
    }

    private void OnBuilderBlur()
    {
        _cameraViewModel.Focus(_lineupCameraAgent);

        foreach (var builderDisplay in _builders)
        {
            builderDisplay.Blur();
        }
    }

    private void OnBuilderFocus(BotBuilder obj)
    {
        foreach (var builderDisplay in _builders)
        {
            if (builderDisplay.BotBuilder == obj)
            {
                builderDisplay.Focus();
                _cameraViewModel.Focus(builderDisplay.CameraAgent);
            }
            else if(builderDisplay.IsFocused)
            {
                builderDisplay.Blur();
            }
        }
    }

    private void OnDestroy()
    {
        _viewModel.OnBuilderAdded -= AddBuilder;
        _viewModel.OnBuilderDismantled -= RemoveBuilder;
        _viewModel.OnBuilderFocus -= OnBuilderFocus;
        _viewModel.OnBuilderBlur -= OnBuilderBlur;
        _viewModel.OnBuilderSelected -= OnBuilderSelected;
    }

    public void RemoveBuilder(BotBuilder builder)
    {
        int index = 0;

        BotBuilderDisplay botBuilderDisplay = _builders.Find((display) => display.BotBuilder == builder);
        _builders.Remove(botBuilderDisplay);
        Destroy(botBuilderDisplay.gameObject);

        for (int i = 0; i < _builders.Count; i++)
        {
            _builders[i].transform.position = new Vector3(0, 0, i * _spacing);
        }
    }

    public void AddBuilder(BotBuilder builder)
    {
        BotBuilderDisplay botBuilderDisplay = Instantiate(_botBuilderDisplayPrefab).GetComponent<BotBuilderDisplay>();
        botBuilderDisplay.transform.SetParent(_listContent, false);
        botBuilderDisplay.gameObject.SetActive(true);
        botBuilderDisplay.Setup(builder);
        botBuilderDisplay.transform.position = new Vector3(0, 0, _builders.Count * _spacing);
        _builders.Add(botBuilderDisplay);
        botBuilderDisplay.OnSelect += OnSelectBuilder;
    }

    private void OnSelectBuilder(BotBuilderDisplay botBuilderDisplay)
    {
        if (botBuilderDisplay.BotBuilder == _viewModel.SelectedBuilder)
        {
            _viewModel.FocusSelectedBuilder();
        }
        else if(!botBuilderDisplay.IsFocused)
        {
            _viewModel.SelectBuilder(botBuilderDisplay.BotBuilder);
        }
    }
}
