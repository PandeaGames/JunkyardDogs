using JunkyardDogs.Components;
using CPU = JunkyardDogs.Specifications.CPU;

namespace JunkyardDogs.Simulation
{
    public abstract class DecisionStartWeaponCharge : DecisionWeapon
    {
        public class DecisionStartWeaponChargeLogic : Logic
        {
            public int wasShotFiredInHowManyDecisions;
            public bool wasShotFiredRecently;
            public int aggressiveness;
        }

        private const int wasShotFiredInHowManyDecisions = 50;

        public DecisionStartWeaponCharge(Chassis.ArmamentLocation weaponArmamentLocation) : base(weaponArmamentLocation)
        {
            
        }

        protected override Logic GetDecisionWeight(SimBot simBot, SimulatedEngagement engagement, Weapon weapon)
        {
            DecisionStartWeaponChargeLogic logic = new DecisionStartWeaponChargeLogic();

            logic.wasShotFiredInHowManyDecisions = wasShotFiredInHowManyDecisions;

            logic.wasShotFiredRecently = simBot.DecisionWasOfType<DecisionWeaponFire>(logic.wasShotFiredInHowManyDecisions);
            logic.aggressiveness = simBot.bot.GetCPUAttribute(CPU.CPUAttribute.Aggressiveness);
            
            if (logic.wasShotFiredRecently)
            {
                logic.weight = -1;
            }
            else
            {
                logic.weight = logic.aggressiveness;
            }

            return logic;
        }

        protected override void MakeDecision(SimBot simBot, SimulatedEngagement engagement, Weapon weapon)
        {
            throw new System.NotImplementedException();
        }
    }

    public class DecisionStartWeaponLeftCharge : DecisionStartWeaponCharge
    {
        public DecisionStartWeaponLeftCharge() : base(Chassis.ArmamentLocation.Left)
        {
            
        }
    }
    
    public class DecisionStartWeaponRightCharge : DecisionStartWeaponCharge
    {
        public DecisionStartWeaponRightCharge() : base(Chassis.ArmamentLocation.Right)
        {
            
        }
    }
    
    public class DecisionStartWeaponTopCharge : DecisionStartWeaponCharge
    {
        public DecisionStartWeaponTopCharge() : base(Chassis.ArmamentLocation.Top)
        {
            
        }
    }
    
    public class DecisionStartWeaponFrontCharge : DecisionStartWeaponCharge
    {
        public DecisionStartWeaponFrontCharge() : base(Chassis.ArmamentLocation.Front)
        {
            
        }
    }
    
    
}