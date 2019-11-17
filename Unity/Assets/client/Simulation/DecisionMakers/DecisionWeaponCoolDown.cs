using JunkyardDogs.Components;

namespace JunkyardDogs.Simulation
{
    public abstract class DecisionWeaponCoolDown : DecisionWeapon 
    {
        public class DecisionWeaponCoolDownLogic : Logic
        {
            public Chassis.ArmamentLocation position;
            public bool isCharging;
        }
        
        public DecisionWeaponCoolDown(Chassis.ArmamentLocation armamentLocation) : base(armamentLocation)
        {
            
        }
        
        protected override Logic GetDecisionWeight(SimBot simBot, SimulatedEngagement engagement, Weapon weapon)
        {
            DecisionWeaponCoolDownLogic logic = new DecisionWeaponCoolDownLogic();
            logic.plane = weapon.GetSpec().DecisionPlane;
            
            if (simBot.IsInWeaponCooldown(logic.plane))
            {
                logic.priority = DecisionPriority.CooldownWeapon;
            }
            else
            {
                logic.priority = DecisionPriority.None;
            }

            return logic;
        }

        protected override void MakeDecision(SimBot simBot, SimulatedEngagement engagement, Weapon weapon)
        {
            //do nohing. we are in cooldown
            engagement.SendEvent(new WeaponCoolDownDecisionEvent(simBot, _armamentLocation));
        }
    }

    public class DecisionWeaponCoolDownLeft : DecisionWeaponCoolDown { public DecisionWeaponCoolDownLeft() : base(Chassis.ArmamentLocation.Left) { } }
    public class DecisionWeaponCoolDownRight : DecisionWeaponCoolDown { public DecisionWeaponCoolDownRight() : base(Chassis.ArmamentLocation.Right) { } }
    public class DecisionWeaponCoolDownTop : DecisionWeaponCoolDown { public DecisionWeaponCoolDownTop() : base(Chassis.ArmamentLocation.Top) { } }
    public class DecisionWeaponCoolDownFront : DecisionWeaponCoolDown { public DecisionWeaponCoolDownFront() : base(Chassis.ArmamentLocation.Front) { } }
}