using UnityEngine;
using System.Collections;
using JunkyardDogs.Specifications;
using System;

namespace JunkyardDogs.Components
{
    [Serializable]
    public class CPU : Component
    {
        public Directive[] Directives { get; set; }

        public CPU()
        {
            
        }

        public Specifications.CPU Spec
        {
            get
            {
                return SpecificationReference.Data as Specifications.CPU;
            }
        }
        
        public int GetAttribute(Specifications.CPU.CPUAttribute attribute)
        {
            return Spec.GetAttribute(attribute);
        }
    }
}