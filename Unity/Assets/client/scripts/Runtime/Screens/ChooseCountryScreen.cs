using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Data;
using System;
using WeakReference = Data.WeakReference;

public class ChooseCountryScreen : ScreenController, ILoadableObject
{
    [SerializeField]
    private GameObject _nationEntryPrefab;

    [SerializeField]
    private SpriteFactory _nationFlagFactory;

    [SerializeField]
    private StringFactory _nationTitleFactory;

    [SerializeField]
    private NationList _nationList;

    [SerializeField]
    private Transform _listContainer;

    private bool _isLoaded = false;

    public bool IsLoaded()
    {
        return _isLoaded;
    }

    public void LoadAsync(Action onLoadSuccess, Action onLoadFailed)
    {
        if (_isLoaded )
        {
            return;
        }

        _nationList.LoadAsync(onLoadSuccess, onLoadFailed);
    }

    private void OnNationListLoaded()
    {

    }

    public override void Start()
    {
        base.Start();

        LoadAsync(LoadComplete, null);
    }

    private void LoadComplete()
    {
        foreach (WeakReference nationalityReference in _nationList)
        {
            Nationality nationality = nationalityReference.Asset as Nationality;
            GameObject nationEntry = Instantiate(_nationEntryPrefab);
            nationEntry.SetActive(true);
            nationEntry.transform.SetParent(_listContainer.transform);
            Image image = nationEntry.GetComponent<Image>();
            Button button = nationEntry.GetComponent<Button>();
            button.onClick.AddListener(() => OnNationClick(nationality));
            image.sprite = _nationFlagFactory.GetAsset(nationality);
        }
    }

    private void OnNationClick(Nationality nationality)
    {
        _window.LaunchScreen("junkyard", ScriptableObject.CreateInstance<JunkyardScreen.Config>());
    }
}