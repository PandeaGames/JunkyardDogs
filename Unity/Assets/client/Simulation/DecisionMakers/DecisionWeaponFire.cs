using JunkyardDogs.Components;

namespace JunkyardDogs.Simulation
{
    public abstract class DecisionWeaponFire : DecisionWeapon 
    {
        public DecisionWeaponFire(Chassis.ArmamentLocation armamentLocation) : base(armamentLocation)
        {
            
        }
        
        protected override Logic GetDecisionWeight(SimBot simBot, SimulatedEngagement engagement, Weapon weapon)
        {
            return new Logic();
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