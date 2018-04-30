using UnityEngine;
using System.Collections;
using JunkyardDogs.Specifications;
using System;

namespace JunkyardDogs.Components
{
    [Serializable]
    public class Motherboard : Component
    {
        [SerializeField]
        public CPU CPU { get; set; }

        public Motherboard()
        {

        }
    }
}