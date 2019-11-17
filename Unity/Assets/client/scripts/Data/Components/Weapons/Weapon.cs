using UnityEngine;
using System.Collections;
using JunkyardDogs.Specifications;
using System;

namespace JunkyardDogs.Components
{
    [Serializable]
    public class Weapon : PhysicalComponent<Specifications.Weapon>
    {
        public Weapon()
        {

        }
    }
}