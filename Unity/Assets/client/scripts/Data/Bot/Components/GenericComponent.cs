using UnityEngine;
using System.Collections;
using JunkyardDogs.Specifications;

namespace JunkyardDogs.Components
{
    public abstract class GenericComponent : ScriptableObject
    {
        public abstract Specification GetSpecification();
    }
}
