using UnityEngine;
using System.Collections;
using JunkyardDogs.Data.Balance;

namespace JunkyardDogs.Specifications
{
    public class PhysicalSpecification : Specification, IStaticDataBalance<PhysicalSpecificationBalanceObject>
    {
        [SerializeField]
        private float _volume;
        public double Volume { get { return _volume; } }

        public void ApplyBalance(PhysicalSpecificationBalanceObject balance)
        {
            this.name = balance.name;
            this._volume = balance.volume;
        }

        public PhysicalSpecificationBalanceObject GetBalance()
        {
            PhysicalSpecificationBalanceObject balance = new PhysicalSpecificationBalanceObject();
            balance.name = this.name;
            balance.volume = _volume;
            return balance;
        }
    }
}