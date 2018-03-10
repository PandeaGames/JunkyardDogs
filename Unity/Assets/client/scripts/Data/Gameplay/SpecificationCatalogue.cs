﻿using UnityEngine;
using System.Collections;
using System;
using JunkyardDogs.Specifications;

[Serializable]
[CreateAssetMenu(fileName = "SpecificationCatalogue", menuName = "GamePlay/SpecificationCatalogue", order = 3)]
public class SpecificationCatalogue : ScriptableObject
{
    [SerializeField]
    private Specification[] _specifications;

    public Specification[] Specifications
    {
        get
        {
            return _specifications;
        }
    }
}
