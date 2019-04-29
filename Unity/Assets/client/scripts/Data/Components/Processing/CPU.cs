using UnityEngine;
using System.Collections;
using JunkyardDogs.Specifications;
using System;

namespace JunkyardDogs.Components
{
    [Serializable]
    public class CPU : Component
    {
        public DirectiveExpansion[] Directives { get; set; }

        public CPU()
        {
            
        }
    }
}