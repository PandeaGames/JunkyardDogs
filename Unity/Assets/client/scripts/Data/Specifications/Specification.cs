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
        
        [SerializeField]
        private float _rarityAndGradeIntrinsicValue;
        
        [SerializeField]
        private float _InstrinsicValue;
        
        [SerializeField]
        private float _rawSellValue;
        
        [SerializeField]
        private float _sellValue;

        public ComponentGrade Grade
        {
            get { return _grade; }
        }
        
        public Rarity Rarity
        {
            get { return _rarity; }
        }
        
        public float RarityAndGradeIntrinsicValue
        {
            get { return _rarityAndGradeIntrinsicValue; }
        }
        
        public float InstrinsicValue
        {
            get { return _InstrinsicValue; }
        }
        
        public float RawSellValue
        {
            get { return _rawSellValue; }
        }
        
        public float SellValue
        {
            get { return _sellValue; }
        }
        
        public void ApplyBalance(SpecificationBalanceObject balance)
        {
            name = balance.name;
            _grade = new ComponentGrade(balance.grade);
            _rarity = new Rarity(balance.rarity);
            _rarityAndGradeIntrinsicValue = balance.rarityAndGradeIntrinsicValue;
            _InstrinsicValue = balance.InstrinsicValue;
            _rawSellValue = balance.rawSellValue;
            _sellValue = balance.sellValue;
        }

        public SpecificationBalanceObject GetBalance()
        {
            SpecificationBalanceObject balance = new SpecificationBalanceObject();
            balance.name = name;
            return balance;
        }
    }
}