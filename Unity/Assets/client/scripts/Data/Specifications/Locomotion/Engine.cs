using JunkyardDogs.Data.Balance;
using UnityEngine;
using UnityEngine.Serialization;

namespace JunkyardDogs.Specifications
{
    public class Engine : PhysicalSpecification, IStaticDataBalance<EngineBalanceObject>
    {
        [SerializeField]
        public float strafeAcceleration;

        [SerializeField]
        public float strafeMaxSpeed;
        
        [SerializeField]
        public float forwardAcceleration;

        [SerializeField]
        public float forwardMaxSpeed;
        
        [SerializeField]
        public float backwardAcceleration;

        [SerializeField]
        public float backwardMaxSpeed;
        
        public void ApplyBalance(EngineBalanceObject balance)
        {
            base.ApplyBalance(balance);
            strafeAcceleration = balance.strafeAcceleration;
            strafeMaxSpeed = balance.strafeMaxSpeed;
            forwardAcceleration = balance.forwardAcceleration;
            forwardMaxSpeed = balance.forwardMaxSpeed;
            backwardAcceleration = balance.backwardAcceleration;
            backwardMaxSpeed = balance.backwardMaxSpeed;
        }

        public EngineBalanceObject GetBalance()
        {
            EngineBalanceObject balance = default(EngineBalanceObject);
            balance.strafeAcceleration = strafeAcceleration;
            balance.strafeMaxSpeed = strafeMaxSpeed;
            return balance;
        }
    }
}