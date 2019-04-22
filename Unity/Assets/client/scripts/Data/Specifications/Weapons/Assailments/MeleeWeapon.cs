using UnityEngine;
using System.Collections;
using JunkyardDogs.Data.Balance;
using JunkyardDogs.Simulation;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace JunkyardDogs.Specifications
{
    [CreateAssetMenu(fileName = "MeleeWeapon", menuName = "Specifications/MeleeWeapon", order = 7)]
    public class MeleeWeapon : Assailer
    {
        [SerializeField]
        private int _range;
        public int Range { get { return _range; } }
    }
}