using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace JunkyardDogs.Components
{
    public enum Attribute
    {
        ATTACK,
        DEFENSE,
        TORQUE, 
        PROCESSING_SPEED
    }

    [Serializable]
    public class Distinction : ScriptableObject
    {
        public double ValueDouble { get; set; }
        public int ValueInteger { get; set; }
        public float ValueFloat { get; set; }

        [SerializeField]
        public Attribute Attribute { get; set; }

    }
}