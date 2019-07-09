using System.Collections.Generic;
using UnityEngine;
using JunkyardDogs.Data.Balance;
using JunkyardDogs.Simulation;

namespace JunkyardDogs.Specifications
{
    [CreateAssetMenu(fileName = "CPU", menuName = "Specifications/CPU", order = 3)]
    public class CPU : Processor, IStaticDataBalance<CPUBalanceObject>
    {
        public enum CPUAttribute
        {
            Aggressiveness,
            Evasiveness,
            Cautiousness
        }
        
        [SerializeField]
        private List<Simulation.Distinction> _distinctions;

        [SerializeField]
        public int Aggressiveness;
        
        [SerializeField]
        public int Evasiveness;
        
        [SerializeField]
        public int Cautiousness;

        [SerializeField]
        public int DirectiveSlotCount;

        public void ApplyBalance(CPUBalanceObject balance)
        {
            _distinctions = new List<Distinction>();
            
            BalanceDataUtils.ProcessDistinction(_distinctions, balance.distinctionId_01, balance.distinctionValue_01);
            BalanceDataUtils.ProcessDistinction(_distinctions, balance.distinctionId_02, balance.distinctionValue_02);
            BalanceDataUtils.ProcessDistinction(_distinctions, balance.distinctionId_03, balance.distinctionValue_03);

            Aggressiveness = balance.aggressiveness;
            Evasiveness = balance.evasiveness;
            Cautiousness = balance.cautiousness;
            DirectiveSlotCount = balance.directiveSlotCount;
        }

        public CPUBalanceObject GetBalance()
        {
            CPUBalanceObject balance = default(CPUBalanceObject);
            balance.name = name;
            return balance;
        }

        public int GetAttribute(CPUAttribute attribute)
        {
            switch (attribute)
            {
                case CPUAttribute.Evasiveness:
                    return Evasiveness;
                case CPUAttribute.Cautiousness:
                    return Cautiousness;
                case CPUAttribute.Aggressiveness:
                    return Aggressiveness;
                default:
                    return 0;
            }
        }
    }
}