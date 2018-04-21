using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Specifications
{
    [CreateAssetMenu(fileName = "MortarShell", menuName = "Specifications/MortarShell", order = 7)]
    public class MortarShell : Projectile
    {
        private int _radius;

        public int Radius { get { return _radius; } }
    }
}