using UnityEngine;
using System.Collections;
using JunkyardDogs.Specifications;
using System.Collections.Generic;
using System;

namespace JunkyardDogs.Components
{
    [Serializable]
    public class Chassis : Component
    {
        public List<Plate> FrontPlates { get; set; }

        [SerializeField]
        public List<Plate> LeftPlates { get; set; }

        [SerializeField]
        public List<Plate> RightPlates { get; set; }

        [SerializeField]
        public List<Plate> BackPlates { get; set; }

        [SerializeField]
        public List<Plate> TopPlates { get; set; }

        [SerializeField]
        public List<Plate> BottomPlates { get; set; }

        [SerializeField]
        public WeaponProcessor TopArmament { get; set; }

        [SerializeField]
        public WeaponProcessor FrontArmament { get; set; }

        [SerializeField]
        public WeaponProcessor LeftArmament { get; set; }

        [SerializeField]
        public WeaponProcessor RightArmament { get; set; }

        public Chassis()
        {

        }
    }
}