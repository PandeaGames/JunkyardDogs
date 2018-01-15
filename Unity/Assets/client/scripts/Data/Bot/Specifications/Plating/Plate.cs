using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Specifications
{
    [CreateAssetMenu(fileName = "Plate", menuName = "Specifications/Plate", order = 4)]
    public class Plate : PhysicalSpecification
    {
        [SerializeField]
        private float _thickness;
    }
}