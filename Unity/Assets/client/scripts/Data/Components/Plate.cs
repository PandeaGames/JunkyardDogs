using UnityEngine;
using System.Collections;
using JunkyardDogs.Specifications;
using System;

namespace JunkyardDogs.Components
{
    [Serializable]
    public class Plate : PhysicalComponent<Specifications.Plate>
    {
        public Plate()
        {

        }
    }
}