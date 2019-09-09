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

        public Directive GetDirective(int index)
        {
            if (Directives == null)
            {
                Directives = new Directive[0];
            }
            
            if (Directives.Length > index)
            {
                return Directives[index];
            }

            return null;
        }
        
        public void SetDirective(int index, Directive directive)
        {
            if (Directives.Length <= index)
            {
                Directive[] temp = Directives;
                Directives = new Directive[index+1];
                temp.CopyTo(Directives,0);
            }

            Directives[index] = directive;
        }
    }
}