using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Behavior
{
    public class Range : ScriptableObject
    {
        [SerializeField]
        private float _radius;

        public float Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }

    }
}