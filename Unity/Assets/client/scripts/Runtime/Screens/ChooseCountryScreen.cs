using UnityEngine;
using System.Collections;
using JunkyardDogs.Data;
using UnityEngine.UI;
using PandeaGames;
using PandeaGames.Views.Screens;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;


public class ChooseCountryScreen : ScreenView
{
    [SerializeField]
    private GameObject _nationEntryPrefab;

    [SerializeField]
    private SpriteFactory _nationFlagFactory;

    [SerializeField]
    private StringFactory _nationTitleFactory;

    [SerializeField]
    private Transform _listContainer;

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
        foreach (NationalityStaticDataReference nationalityReference in _viewModel.NationList)
        {
            Nationality nationality = nationalityReference.Data;
            GameObject nationEntry = Instantiate(_nationEntryPrefab);
            nationEntry.SetActive(true);
            nationEntry.transform.SetParent(_listContainer.transform);
            Image image = nationEntry.GetComponent<Image>();
            Button button = nationEntry.GetComponent<Button>();
            button.onClick.AddListener(() => OnNationClick(nationalityReference));
            image.sprite = _nationFlagFactory.GetAsset(nationality);
        }
    }

    private void OnNationClick(NationalityStaticDataReference nationalityReference)
    {
        //_junkyardUserService.User.Competitor.Nationality = nationalityReference;
        _viewModel.SetChosenNationality(nationalityReference);
        //_junkyardUserService.Save();
        //_window.LaunchScreen("junkyard");
    }
}