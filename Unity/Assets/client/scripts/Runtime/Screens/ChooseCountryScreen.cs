using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JunkyardDogs.Data;
using UnityEngine.UI;
using PandeaGames;
using PandeaGames.Runtime.Dialogs.ViewModels;
using PandeaGames.Views.Screens;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;


public class ChooseCountryScreen : ScreenView
{
    [SerializeField]
    private NationalityListView _listView;

    private JunkyardUserService _junkyardUserService;
    private ChooseNationalityViewModel _viewModel;

    public override void Setup(WindowView window)
    {
        base.Setup(window);
        _viewModel = Game.Instance.GetViewModel<ChooseNationalityViewModel>(0);
        LoadComplete();
    }

    private void LoadComplete()
    {
        NationalityDataProvider provider = Game.Instance.GetStaticDataPovider<NationalityDataProvider>();
        
        _listView.SetData(provider);
        _listView.OnItemSelected += OnNationClick;
    }

    private void OnNationClick(NationalityStaticDataReference nationalityReference)
    {
        _viewModel.RequestChosenNationality(nationalityReference);
    }
}