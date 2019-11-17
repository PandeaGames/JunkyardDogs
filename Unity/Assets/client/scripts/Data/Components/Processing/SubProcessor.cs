using UnityEngine;
using System.Collections;
using JunkyardDogs.Specifications;
using System;

namespace JunkyardDogs.Components
{
    [Serializable]
    public abstract class SubProcessor<TSpecification> : Component <TSpecification> where TSpecification:SubProcessor
    {
        public SubProcessor()
        {

        }
    }
}