using JunkyardDogs.Components;
using Weapon = JunkyardDogs.Specifications.Weapon;

namespace JunkyardDogs.Simulation
{
    public class SimMeleeAttack : SimPhysicalAttackObject
    {
        public SimMeleeAttack(SimulatedEngagement engagement, SimBot simBot, Chassis.ArmamentLocation armementLocation) : base(engagement, simBot, armementLocation)
        {
            collider = CreateCollider(simBot.bot.GetArmament(armementLocation).GetSpec<Weapon>());
        }

        public override void OnSimEvent(SimulatedEngagement engagement, SimPostLogicEvent simEvent)
        {
            base.OnSimEvent(engagement, simEvent);
            engagement.MarkForRemoval(this);
        }
    }
}