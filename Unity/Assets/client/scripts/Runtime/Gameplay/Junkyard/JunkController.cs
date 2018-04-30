using JunkyardDogs.Specifications;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JunkyardDogs.Components;
using Random = UnityEngine.Random;
using WeakReference = Data.WeakReference;
using Component = JunkyardDogs.Components.Component;

public class JunkController : MonoBehaviour {

    [SerializeField]
    private ServiceManager _serviceManager;

    [SerializeField]
    private Junk[] _junkList;

    private SpecificationCatalogue _specificationCatalogue;
    private WeakReference[] _specifications;
    private WeakReference _specification;
    private DialogService _dialogService; 
    private JunkyardUserService _userService;
    private ComponentInstantiatorService _componentService;
    private JunkyardUser _user;

    protected void Start()
    {
        _dialogService = _serviceManager.GetService<DialogService>();
        _userService = _serviceManager.GetService<JunkyardUserService>();
        _componentService = _serviceManager.GetService<ComponentInstantiatorService>();
        _user = _userService.User;
        
        foreach (Junk junk in _junkList)
        {
            junk.OnClick += HandleJunkClick;
        }
    }
    
    private void HandleJunkClick(Junk junk)
    {
        _specificationCatalogue = junk.Catalogue;
        _specifications = _specificationCatalogue.Specifications;

        int length = _specifications.Length;
        int choice = (int)Random.Range(0, length);

        _specification = _specifications[choice];

        _specification.LoadAsync<Specification>(OnLoadComplete, OnLoadFail);

        Destroy(junk.gameObject);
    }

    private void OnLoadComplete(Specification asset, WeakReference reference)
    {
        Component component = _componentService.GenerateComponent(_specification);

        component.Manufacturer = _specificationCatalogue.Manufacturer;

        TakeJunkDialog.TakeJunkDialogConfig config = ScriptableObject.CreateInstance<TakeJunkDialog.TakeJunkDialogConfig>();

        config.Component = component;

        _dialogService.DisplayDialog<TakeJunkDialog>(config, SelectComponent);
    }

    private void OnLoadFail()
    {

    }

    private void SelectComponent(Dialog.Response response)
    {
        TakeJunkDialog.TakeJunkDialogResponse dialogReponse = response as TakeJunkDialog.TakeJunkDialogResponse;

        _user.AddComponent(dialogReponse.Component);
        _userService.Save();
    }
}
