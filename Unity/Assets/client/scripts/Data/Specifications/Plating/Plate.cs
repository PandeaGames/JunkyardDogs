using UnityEngine;
using System.Collections;
using JunkyardDogs.Data.Balance;

namespace JunkyardDogs.Specifications
{
    [CreateAssetMenu(fileName = "Plate", menuName = "Specifications/Plate", order = 4)]
    public class Plate : PhysicalSpecification, IStaticDataBalance<PlateBalanceObject>
    {
        [SerializeField]
        private float _thickness;
        
        public void ApplyBalance(PlateBalanceObject balance)
        {
            this.name = balance.name;
            this._volume = balance.volume;
            _thickness = balance.thickness;
        }

        public PlateBalanceObject GetBalance()
        {
            PlateBalanceObject balance = new PlateBalanceObject();
            balance.name = this.name;
            balance.volume = _volume;
            balance.thickness = _thickness;
            return balance;
        }
    }
}