using System;
using JunkyardDogs.Components;
using UnityEngine.Scripting;
using UnityEngine.UI.Extensions;

namespace JunkyardDogs.Simulation
{
    [Preserve]
    public abstract class DecisionWeaponFire : DecisionWeapon
    {
        public class DecisionWeaponFireLogic : Logic
        {
            public int lastWeaponChargeDecisionTick = -1;
            public bool isChargingSameWeapon;
            public double lastWeaponChargeTime = -1;
            public bool wasChargingWeapon;
            public int chargeTicks;
            public double chargeTime;
            public bool isWeaponChargeComplete;
        }
        
        
        public DecisionWeaponFire(Chassis.ArmamentLocation armamentLocation) : base(armamentLocation)
        {
            
    
        }
        
        protected override Logic GetDecisionWeight(SimBot simBot, SimulatedEngagement engagement, Weapon weapon)
        {
            DecisionWeaponFireLogic logic = new DecisionWeaponFireLogic();
            logic.plane = weapon.GetSpec().DecisionPlane;

            logic.wasChargingWeapon =
                simBot.IsLastDecisionOfType<DecisionStartWeaponCharge>(new Type[] {typeof(DecisionWeaponCharge)}, logic.plane);

            if (logic.wasChargingWeapon)
            {
                SimBotDecisionPlane.WeightedDecision lastStartWeaponChargeDecision =
                    simBot.GetLastWeightedDecisionOfType<DecisionStartWeaponCharge>(logic.plane);
                DecisionStartWeaponCharge decisionStartWeaponCharge =
                    lastStartWeaponChargeDecision.DecisionMaker as DecisionStartWeaponCharge;

                logic.isChargingSameWeapon = decisionStartWeaponCharge.armamentLocation == _armamentLocation;

                if (logic.isChargingSameWeapon)
                {
                    logic.lastWeaponChargeDecisionTick = lastStartWeaponChargeDecision.simulationTick;
                    logic.lastWeaponChargeTime = decisionStartWeaponCharge.GetWeapon(simBot)
                        .GetSpec().ChargeTime;

                    logic.chargeTicks = engagement.CurrentStep - logic.lastWeaponChargeDecisionTick;
                    logic.chargeTime = engagement.ConvertStepsToSeconds(logic.chargeTicks);
                    logic.isWeaponChargeComplete = logic.chargeTime > logic.lastWeaponChargeTime;
                }

                logic.priority = logic.isWeaponChargeComplete ? DecisionPriority.FireWeapon : DecisionPriority.None;
            }
            else
            {
                logic.priority = DecisionPriority.None;
            }
            
            return logic;
        }

        protected override void MakeDecision(SimBot simBot, SimulatedEngagement engagement, Weapon weapon)
        {
            simBot.FireWeapon(_armamentLocation);
            engagement.SendEvent(new WeaponFireDecisionEvent(simBot, _armamentLocation));
        }
    }

    [Preserve] public class DecisionWeaponLeft : DecisionWeaponFire { public DecisionWeaponLeft() : base(Chassis.ArmamentLocation.Left) { } }
    [Preserve] public class DecisionWeaponRight : DecisionWeaponFire { public DecisionWeaponRight() : base(Chassis.ArmamentLocation.Right) { } }
    [Preserve] public class DecisionWeaponTop : DecisionWeaponFire { public DecisionWeaponTop() : base(Chassis.ArmamentLocation.Top) { } }
    [Preserve] public class DecisionWeaponFront : DecisionWeaponFire { public DecisionWeaponFront() : base(Chassis.ArmamentLocation.Front) { } }
}