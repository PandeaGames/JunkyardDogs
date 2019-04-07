using JunkyardDogs.Specifications;
using System;
using System.Collections;
using System.Collections.Generic;
using JunkyardDogs;
using UnityEngine;
using JunkyardDogs.Components;
using JunkyardDogs.Data;
using JunkyardDogs.scripts.Runtime.Dialogs;
using PandeaGames;
using Random = UnityEngine.Random;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;
public class JunkController : MonoBehaviour {

    [SerializeField]
    private ServiceManager _serviceManager;

    [SerializeField]
    private Junk[] _junkList;

    private LootCrateStaticDataReference _lootCrate;
    private SpecificationCatalogue.Product[] _products;
    private SpecificationCatalogue.Product _product;
    private JunkyardUserService _userService;
    private JunkyardViewModel _junkyardViewModel;

    protected void Start()
    {
        _userService = Game.Instance.GetService<JunkyardUserService>();
        _junkyardViewModel = Game.Instance.GetViewModel<JunkyardViewModel>(0);
        
        foreach (Junk junk in _junkList)
        {
            junk.OnClick += HandleJunkClick;
        }
    }

    private void HandleJunkClick(Junk junk)
    {
        _lootCrate = junk.LootCrate;
        Destroy(junk.gameObject);
        _junkyardViewModel.TakeJunk(_lootCrate);
    }
}
