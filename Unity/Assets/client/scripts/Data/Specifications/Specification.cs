using UnityEngine;
using System.Collections;
using JunkyardDogs.Components;
using JunkyardDogs.Data.Balance;

namespace JunkyardDogs.Specifications
{
    public class Specification : AbstractStaticData, IStaticDataBalance<SpecificationBalanceObject>
    {
        [SerializeField]
        private ComponentGrade _grade;

        [SerializeField]
        private Rarity _rarity;

        public ComponentGrade Grade
        {
            get { return _grade; }
        }
        
        public Rarity Rarity
        {
            get { return _rarity; }
        }
        
        public void ApplyBalance(SpecificationBalanceObject balance)
        {
            name = balance.name;
            _grade = new ComponentGrade(balance.grade);
            _rarity = new Rarity(balance.rarity);
        }

        public SpecificationBalanceObject GetBalance()
        {
            SpecificationBalanceObject balance = new SpecificationBalanceObject();
            balance.name = this.name;
            return balance;
        }
        
    }
}