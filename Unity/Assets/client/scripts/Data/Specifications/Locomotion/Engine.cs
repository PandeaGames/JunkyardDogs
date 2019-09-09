using JunkyardDogs.Data.Balance;
using UnityEngine;

namespace JunkyardDogs.Specifications
{
    public class Engine : PhysicalSpecification, IStaticDataBalance<EngineBalanceObject>
    {
        [SerializeField]
        public float acceleration;

        [SerializeField]
        public float maxSpeed;

        public void ApplyBalance(EngineBalanceObject balance)
        {
            acceleration = balance.acceleration;
            maxSpeed = balance.maxSpeed;
        }

        public EngineBalanceObject GetBalance()
        {
            EngineBalanceObject balance = default(EngineBalanceObject);
            balance.acceleration = acceleration;
            balance.maxSpeed = maxSpeed;
            return balance;
        }
    }
}