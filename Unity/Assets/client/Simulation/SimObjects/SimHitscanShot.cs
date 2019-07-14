using JunkyardDogs.Specifications;
using Chassis = JunkyardDogs.Components.Chassis;

namespace JunkyardDogs.Simulation
{
    public class SimHitscanShot : SimPhysicalAttackObject
    {
        private Hitscan hitscan;
        public SimHitscanShot(SimulatedEngagement engagement, SimBot simBot, Chassis.ArmamentLocation armementLocation) : base(engagement, simBot, armementLocation)
        {
            hitscan = simBot.bot.GetArmament(armementLocation).GetSpec<Hitscan>();
            collider = CreateCollider(hitscan);
        }

        public override void OnSimEvent(SimulatedEngagement engagement, SimPostLogicEvent simEvent)
        {
            base.OnSimEvent(engagement, simEvent);
            engagement.MarkForRemoval(this);
        }
    }
}