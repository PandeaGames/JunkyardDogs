using System;
using PandeaGames.Entities;
using UnityEngine;

namespace JunkyardDogs.Components.Gameplay
{
    [Serializable]
    public class Exp : ValueContainer<Exp, int>
    {
        public Exp()
        {
            
        }
        
        public Exp(int exp) : base(exp)
        {
        }

        public static Exp operator +(Exp exp1, Exp exp2)
        {
            return new Exp(exp1._value + exp2._value);
        }
        
        public static Exp operator -(Exp exp1, Exp exp2)
        {
            return new Exp(exp1._value - exp2._value);
        }
        
        public static implicit operator int(Exp exp)
        {
            return exp._value;
        }
        
        public static implicit operator Exp(int exp)
        {
            return new Exp(exp);
        }

        public override Exp Add(Exp other)
        {
            return this + other;
        }
    }
}