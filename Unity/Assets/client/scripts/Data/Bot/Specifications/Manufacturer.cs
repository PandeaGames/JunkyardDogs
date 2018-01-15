using UnityEngine;
using System.Collections;
using JunkyardDogs.Components;

namespace JunkyardDogs.Specifications
{
    [CreateAssetMenu(fileName = "Manufacturer", menuName = "Specifications/Manufacturer", order = 2)]
    public class Manufacturer : ScriptableObject
    {
        [SerializeField]
        private Distinction _distinctions;
    }
}