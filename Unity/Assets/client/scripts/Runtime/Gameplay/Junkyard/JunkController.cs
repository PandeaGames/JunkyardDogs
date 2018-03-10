using JunkyardDogs.Specifications;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class JunkController : MonoBehaviour {

    [SerializeField]
    private ServiceManager _serviceManager;

    [SerializeField]
    private Junk[] _junkList;

    private SpecificationCatalogue _specificationCatalogue;
    private Specification[] _specifications;
    private Specification _specification;

    protected void Start()
    {
        foreach(Junk junk in _junkList)
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

        Destroy(junk.gameObject);
    }

    private void SelectComponent(Specification spec)
    {
        


    }
}
