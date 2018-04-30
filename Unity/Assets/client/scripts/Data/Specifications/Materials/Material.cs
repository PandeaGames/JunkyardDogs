using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Specifications
{
    [CreateAssetMenu(fileName = "Material", menuName = "Specifications/Material", order = 2)]
    public class Material
    {
        [SerializeField]
        private double _density;
    }
}