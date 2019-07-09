using JunkyardDogs.Data.Balance;
using UnityEngine;

namespace JunkyardDogs.Specifications
{
    [CreateAssetMenu(fileName = "Directive", menuName = "Specifications/Directive", order = 3)]
    public class Directive : SubProcessor, IStaticDataBalance<DirectiveBalanceObject>
    {
        public Simulation.Directive SimulatedDirective;
        
        public void ApplyBalance(DirectiveBalanceObject balance)
        {
            name = balance.name;
            SimulatedDirective = BalanceDataUtilites.DecodeEnumSingle<Simulation.Directive>(balance.directive);
        }

        public DirectiveBalanceObject GetBalance()
        {
            DirectiveBalanceObject balance = default(DirectiveBalanceObject);
            balance.name = name;
            balance.directive = BalanceDataUtilites.EncodeEnumSingle(SimulatedDirective);
            return balance;
        }
    }
}