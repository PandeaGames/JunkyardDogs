using UnityEngine;
using UnityEditor;

namespace JunkyardDogs.Specifications
{
    [CreateAssetMenu(fileName = "Wheel", menuName = "Specifications/Wheel", order = 2)]
    public class Wheel : Specification
    {
        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float _friction = 0;

        [SerializeField]
        private float _radius;

        public float Friction { get { return _friction; } set { _friction = value; } }
        public float Radius { get { return _radius; } set { _friction = value; } }
    }
}