using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JunkyardDogs.Bot
{
    public enum Attribute
    {
        ATTACK,
        DEFENSE
    }

    public class Distinction : ScriptableObject
    {
        private double _valueDouble;
        private int _valueInteger;
        private float _valueFloat;

        [SerializeField]
        private Attribute _attribute;

        public Attribute Attribute { get { return _attribute; } }
    }
}