using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Specifications
{
    public class Weapon : PhysicalSpecification
    {
        private int _chargeTime;

        public int ChargeTime { get { return _chargeTime; } }
    }
}