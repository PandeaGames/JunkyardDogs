using UnityEngine;
using System.Collections;
using JunkyardDogs.Data.Balance;

namespace JunkyardDogs.Specifications
{
    public class Specification : AbstractStaticData, IStaticDataBalance<SpecificationBalanceObject>
    {
        public void ApplyBalance(SpecificationBalanceObject balance)
        {
            this.name = balance.name;
        }

        public SpecificationBalanceObject GetBalance()
        {
            SpecificationBalanceObject balance = new SpecificationBalanceObject();
            balance.name = this.name;
            return balance;
        }
        
    }
}