using System;
using JunkyardDogs.Specifications;
using UnityEngine;
using UnityEngine.Scripting;
using Chassis = JunkyardDogs.Components.Chassis;
using CPU = JunkyardDogs.Specifications.CPU;
using Weapon = JunkyardDogs.Components.Weapon;

namespace JunkyardDogs.Simulation
{
    [Preserve]
    public abstract class DecisionStartWeaponCharge : DecisionWeapon
    {
        public class DecisionStartWeaponChargeLogic : Logic
        {
            public int aggressiveness;
            public float distance;
            public bool isMeleeWeapon;
            public float meleeDistance;
            public bool isWithinMeleeRange;
            public bool wasLastDecisionAWeapon;
        }

        public DecisionStartWeaponCharge(Chassis.ArmamentLocation weaponArmamentLocation) : base(weaponArmamentLocation)
        {
            
        }

        protected override Logic GetDecisionWeight(SimBot simBot, SimulatedEngagement engagement, Weapon weapon)
        {
            DecisionStartWeaponChargeLogic logic = new DecisionStartWeaponChargeLogic();
            Melee meleeWeapon = weapon is Melee ? (Melee) weapon.GetSpec():null;
            logic.plane = weapon.GetSpec().DecisionPlane;
            logic.distance = (int) Vector2.Distance(simBot.body.position, simBot.opponent.body.position);
            logic.wasLastDecisionAWeapon = simBot.IsLastDecisionOfType<DecisionWeapon>(DecisionPlane.Base);
            
            logic.isMeleeWeapon = meleeWeapon != null;
            logic.meleeDistance = simBot.GetBounds().width / 2 + 1;
            logic.isWithinMeleeRange = logic.distance < logic.meleeDistance;

            if (logic.isMeleeWeapon && !logic.isWithinMeleeRange
                || !logic.isMeleeWeapon && logic.isWithinMeleeRange
                || logic.wasLastDecisionAWeapon)
            {
                logic.priority = DecisionPriority.None;
            }
            else
            {
                logic.aggressiveness = simBot.bot.GetCPUAttribute(CPU.CPUAttribute.Aggressiveness);
                logic.weight = logic.aggressiveness;
            }
            
            return logic;
        }

        protected override void MakeDecision(SimBot simBot, SimulatedEngagement engagement, Weapon weapon)
        {
            simBot.body.rotation.SetFromToRotation( simBot.body.position, simBot.opponent.body.position);
            engagement.SendEvent(new WeaponStartChargeDecisionEvent(simBot, _armamentLocation));
        }
    }

    [Preserve]
    public class DecisionStartWeaponLeftCharge : DecisionStartWeaponCharge
    {
        public DecisionStartWeaponLeftCharge() : base(Chassis.ArmamentLocation.Left)
        {
            
        }
    }
    
    [Preserve]
    public class DecisionStartWeaponRightCharge : DecisionStartWeaponCharge
    {
        public DecisionStartWeaponRightCharge() : base(Chassis.ArmamentLocation.Right)
        {
            
        }
    }
    
    [Preserve]
    public class DecisionStartWeaponTopCharge : DecisionStartWeaponCharge
    {
        public DecisionStartWeaponTopCharge() : base(Chassis.ArmamentLocation.Top)
        {
            
        }
    }
    
    [Preserve]
    public class DecisionStartWeaponFrontCharge : DecisionStartWeaponCharge
    {
        public DecisionStartWeaponFrontCharge() : base(Chassis.ArmamentLocation.Front)
        {
            
        }
    }
    
    
}