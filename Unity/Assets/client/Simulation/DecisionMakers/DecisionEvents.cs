using JunkyardDogs.Components;
using UnityEngine;

namespace JunkyardDogs.Simulation
{
    public abstract class DecisionEvent : SimLogicEvent
    {
        public readonly SimBot simBot;

        public DecisionEvent(SimBot simBot)
        {
            this.simBot = simBot;
        }
    }

    public abstract class WeaponDecisionEvent : DecisionEvent
    {
        public readonly Chassis.ArmamentLocation armamentLocation;

        public WeaponDecisionEvent(SimBot simBot, Chassis.ArmamentLocation armamentLocation) : base(simBot)
        {
            this.armamentLocation = armamentLocation;
        }
    }
    
    public class WeaponStartChargeDecisionEvent : WeaponDecisionEvent
    {
        public WeaponStartChargeDecisionEvent(SimBot simBot, Chassis.ArmamentLocation armamentLocation) : base(simBot, armamentLocation) {}
    }
    
    public class WeaponChargeDecisionEvent : WeaponDecisionEvent
    {
        public WeaponChargeDecisionEvent(SimBot simBot, Chassis.ArmamentLocation armamentLocation) : base(simBot, armamentLocation) {}
    }
    
    public class WeaponFireDecisionEvent : WeaponDecisionEvent
    {
        public WeaponFireDecisionEvent(SimBot simBot, Chassis.ArmamentLocation armamentLocation) : base(simBot, armamentLocation) {}
    }
    
    public class WeaponCoolDownDecisionEvent : WeaponDecisionEvent
    {
        public WeaponCoolDownDecisionEvent(SimBot simBot, Chassis.ArmamentLocation armamentLocation) : base(simBot, armamentLocation) {}
    }
    
    public class StunDecisionEvent : DecisionEvent
    {
        public StunDecisionEvent(SimBot simBot) : base(simBot) {}
    }
    
    public class MoveDecisionEvent : DecisionEvent
    {
        public readonly Vector2 vector;
        public MoveDecisionEvent(SimBot simBot, Vector2 vector) : base(simBot)
        {
            this.vector = vector;
        }
    }
}