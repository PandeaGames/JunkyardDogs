using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Bot
{
    public class Weapon : BodyComponent
    {
        private int _chargeTime;

        public int ChargeTime { get { return _chargeTime; } }
    }
}