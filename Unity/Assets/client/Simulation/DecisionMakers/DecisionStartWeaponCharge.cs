using System;
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
            public int stepsSinceLastCooldown;
            public int aggressiveness;
            public bool hasCooldown;
            public int ticksSinceLastWeaponFire;
        }

        private const int wasShotFiredInHowManyDecisions = 50;

        public DecisionStartWeaponCharge(Chassis.ArmamentLocation weaponArmamentLocation) : base(weaponArmamentLocation)
        {
            
        }

        protected override Logic GetDecisionWeight(SimBot simBot, SimulatedEngagement engagement, Weapon weapon)
        {
            DecisionStartWeaponChargeLogic logic = new DecisionStartWeaponChargeLogic();

            logic.wasShotFiredInHowManyDecisions = wasShotFiredInHowManyDecisions;
            logic.ticksSinceLastWeaponFire = simBot.TicksSinceLastDecisionOfType<DecisionWeaponFire>();

            logic.wasShotFiredRecently = simBot.DecisionWasOfType<DecisionWeaponFire>(logic.wasShotFiredInHowManyDecisions);
            logic.wasShotFiredRecently |= simBot.DecisionWasOfType<DecisionStartWeaponCharge>(logic.wasShotFiredInHowManyDecisions);
            logic.wasShotFiredRecently |= simBot.DecisionWasOfType<DecisionWeaponCharge>(logic.wasShotFiredInHowManyDecisions);
            logic.wasShotFiredRecently |= simBot.DecisionWasOfType<DecisionWeaponCoolDown>(logic.wasShotFiredInHowManyDecisions);
            logic.aggressiveness = simBot.bot.GetCPUAttribute(CPU.CPUAttribute.Aggressiveness);

            logic.weight += logic.ticksSinceLastWeaponFire;
            
            if (logic.wasShotFiredRecently)
            {
                logic.priority = (int) DecisionPriority.None;
            }
            else
            {
                SimBot.WeightedDecision cooldownDecisionDecision =
                    simBot.GetLastWeightedDecisionOfType<DecisionWeaponCoolDown>();
                logic.hasCooldown = cooldownDecisionDecision != null;

                if (logic.hasCooldown)
                {
                    logic.stepsSinceLastCooldown =
                        simBot.GetLastWeightedDecisionOfType<DecisionWeaponCoolDown>().simulationTick;
                    logic.weight = (logic.aggressiveness * (1 + logic.stepsSinceLastCooldown/10)) + logic.stepsSinceLastCooldown;
                }
                
               
                logic.weight = logic.aggressiveness;
            }

            return logic;
        }

        protected override void MakeDecision(SimBot simBot, SimulatedEngagement engagement, Weapon weapon)
        {
            engagement.SendEvent(new WeaponStartChargeDecisionEvent(simBot, _armamentLocation));
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