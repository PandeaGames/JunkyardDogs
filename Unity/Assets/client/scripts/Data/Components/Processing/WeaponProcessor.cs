using UnityEngine;
using System.Collections;
using JunkyardDogs.Specifications;
using System;

namespace JunkyardDogs.Components
{

    [Serializable]
    public class WeaponProcessor : PhysicalComponent
    {
        public Components.Weapon Weapon { get; set; }

        public WeaponProcessor()
        {

        }
    }
}