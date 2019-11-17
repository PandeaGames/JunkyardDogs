using JunkyardDogs.Components;

namespace JunkyardDogs.Simulation
{
    public abstract class DecisionWeaponCharge : DecisionWeapon 
    {
        public class DecisionWeaponChargeLogic : Logic
        {
            public Chassis.ArmamentLocation position;
            public bool isCharging;
        }
        
        public DecisionWeaponCharge(Chassis.ArmamentLocation armamentLocation) : base(armamentLocation)
        {
            
        }
        
        protected override Logic GetDecisionWeight(SimBot simBot, SimulatedEngagement engagement, Weapon weapon)
        {
            DecisionWeaponChargeLogic logic = new DecisionWeaponChargeLogic();
            logic.plane = weapon.GetSpec().DecisionPlane;
            
            if (simBot.IsChargingWeapon(_armamentLocation, logic.plane))
            {
                logic.priority = DecisionPriority.PoweringWeapon;
            }
            else
            {
                logic.priority = DecisionPriority.None;
            }

            return logic;
        }

        protected override void MakeDecision(SimBot simBot, SimulatedEngagement engagement, Weapon weapon)
        {
            //do nothing. weapon is charging
            engagement.SendEvent(new WeaponChargeDecisionEvent(simBot, _armamentLocation));
            simBot.body.rotation.SetFromToRotation( simBot.body.position, simBot.opponent.body.position);
        }
    }

    public class DecisionWeaponChargeLeft : DecisionWeaponCharge { public DecisionWeaponChargeLeft() : base(Chassis.ArmamentLocation.Left) { } }
    public class DecisionWeaponChargeRight : DecisionWeaponCharge { public DecisionWeaponChargeRight() : base(Chassis.ArmamentLocation.Right) { } }
    public class DecisionWeaponChargeTop : DecisionWeaponCharge { public DecisionWeaponChargeTop() : base(Chassis.ArmamentLocation.Top) { } }
    public class DecisionWeaponChargeFront : DecisionWeaponCharge { public DecisionWeaponChargeFront() : base(Chassis.ArmamentLocation.Front) { } }
}