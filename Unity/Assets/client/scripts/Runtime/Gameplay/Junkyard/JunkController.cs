using JunkyardDogs.Specifications;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JunkyardDogs.Components;
using JunkyardDogs.scripts.Runtime.Dialogs;
using PandeaGames;
using Random = UnityEngine.Random;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;
public class JunkController : MonoBehaviour {

    [SerializeField]
    private ServiceManager _serviceManager;

    [SerializeField]
    private Junk[] _junkList;

    private SpecificationCatalogue _specificationCatalogue;
    private SpecificationCatalogue.Product[] _products;
    private SpecificationCatalogue.Product _product;
    private DialogService _dialogService; 
    private JunkyardUserService _userService;
    private ComponentInstantiatorService _componentService;
    private JunkyardUser _user;

    protected void Start()
    {
        _dialogService = _serviceManager.GetService<DialogService>();
        _userService = Game.Instance.GetService<JunkyardUserService>();
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
        _products = _specificationCatalogue.Products;

        int length = _products.Length;
        int choice = (int)Random.Range(0, length);

        _product = _products[choice];

        _product.LoadAsync(OnLoadComplete, OnLoadFail);

        Destroy(junk.gameObject);
    }

    private void OnLoadComplete()
    {
        ManufacturerUtils.BuildComponent(_specificationCatalogue.Manufacturer, _product, (component) =>
        {
            TakeJunkDialogViewModel vm = Game.Instance.GetViewModel<TakeJunkDialogViewModel>(0);
            vm.SetData(new TakeJunkDialogViewModel.Data(component));
            //vm.OnClose 
            _dialogService.DisplayDialog<TakeJunkDialog>(vm);
        });
    }

    private void OnLoadFail(LoadException e)
    {

    }

    private void SelectComponent()
    {
        TakeJunkDialogViewModel vm = Game.Instance.GetViewModel<TakeJunkDialogViewModel>(0);
        //_user.AddComponent(dialogReponse.Component);
        //_userService.Save();
    }
}
