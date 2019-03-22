using UnityEngine;
using System.Collections;
using JunkyardDogs.Data.Balance;

namespace JunkyardDogs.Specifications
{
    [CreateAssetMenu(fileName = "Material", menuName = "Specifications/Material", order = 2)]
    public class Material : ScriptableObject, IStaticDataBalance<MaterialBalanceObject>
    {
        [SerializeField]
        private double _density;

        public void ApplyBalance(MaterialBalanceObject balance)
        {
            this.name = balance.name;
            _density = balance.density;
        }
        
        public MaterialBalanceObject GetBalance()
        {
            MaterialBalanceObject balance = new MaterialBalanceObject();
            balance.name = this.name;
            balance.density = _density;
            return balance;
        }
    }
}