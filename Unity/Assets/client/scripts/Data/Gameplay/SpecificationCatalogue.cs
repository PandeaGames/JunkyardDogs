using UnityEngine;
using System.Collections;
using System;
using JunkyardDogs.Specifications;
using WeakReference = Data.WeakReference;

[Serializable]
[CreateAssetMenu(fileName = "SpecificationCatalogue", menuName = "GamePlay/SpecificationCatalogue", order = 3)]
public class SpecificationCatalogue : ScriptableObject
{
    [SerializeField][WeakReference(typeof(Manufacturer))]
    private WeakReference _manufacturer;

    public WeakReference Manufacturer
    {
        get
        {
            return _manufacturer;
        }
    }

    [SerializeField][WeakReference(typeof(Specification))]
    private WeakReference[] _specifications;

    public WeakReference[] Specifications
    {
        get
        {
            return _specifications;
        }
    }
}
