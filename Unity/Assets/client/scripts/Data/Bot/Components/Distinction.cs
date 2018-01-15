using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JunkyardDogs.Components
{
    public enum Attribute
    {
        ATTACK,
        DEFENSE,
        TORQUE, 
        PROCESSING_SPEED
    }

    [CreateAssetMenu(fileName = "Distinction", menuName = "Components/Distinction", order = 2)]
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