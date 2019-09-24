using System;
using JunkyardDogs.Components;
using CPU = JunkyardDogs.Specifications.CPU;

namespace JunkyardDogs.Simulation
{
    public abstract class DecisionStartWeaponCharge : DecisionWeapon
    {
        public class DecisionStartWeaponChargeLogic : Logic
        {
            public int aggressiveness;
        }

        public DecisionStartWeaponCharge(Chassis.ArmamentLocation weaponArmamentLocation) : base(weaponArmamentLocation)
        {
            
        }

        protected override Logic GetDecisionWeight(SimBot simBot, SimulatedEngagement engagement, Weapon weapon)
        {
            DecisionStartWeaponChargeLogic logic = new DecisionStartWeaponChargeLogic();

            logic.aggressiveness = simBot.bot.GetCPUAttribute(CPU.CPUAttribute.Aggressiveness);
            logic.weight = logic.aggressiveness;

            return logic;
        }

        protected override void MakeDecision(SimBot simBot, SimulatedEngagement engagement, Weapon weapon)
        {
            simBot.body.rotation.SetFromToRotation( simBot.body.position, simBot.opponent.body.position);
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