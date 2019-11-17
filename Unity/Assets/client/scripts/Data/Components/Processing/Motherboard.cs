using UnityEngine;
using System.Collections;
using JunkyardDogs.Specifications;
using System;

namespace JunkyardDogs.Components
{
    [Serializable]
    public class Motherboard : Component<Specifications.Motherboard>
    {
        [SerializeField]
        public CPU CPU { get; set; }

        public Motherboard()
        {

        }
    }
}