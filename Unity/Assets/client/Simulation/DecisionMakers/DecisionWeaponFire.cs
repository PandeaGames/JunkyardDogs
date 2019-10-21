using System;
using JunkyardDogs.Components;
using UnityEngine.UI.Extensions;

namespace JunkyardDogs.Simulation
{
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
            logic.plane = weapon.GetSpec<Specifications.Weapon>().DecisionPlane;

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
                        .GetSpec<Specifications.Weapon>().ChargeTime;

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

    public class DecisionWeaponLeft : DecisionWeaponFire { public DecisionWeaponLeft() : base(Chassis.ArmamentLocation.Left) { } }
    public class DecisionWeaponRight : DecisionWeaponFire { public DecisionWeaponRight() : base(Chassis.ArmamentLocation.Right) { } }
    public class DecisionWeaponTop : DecisionWeaponFire { public DecisionWeaponTop() : base(Chassis.ArmamentLocation.Top) { } }
    public class DecisionWeaponFront : DecisionWeaponFire { public DecisionWeaponFront() : base(Chassis.ArmamentLocation.Front) { } }
}