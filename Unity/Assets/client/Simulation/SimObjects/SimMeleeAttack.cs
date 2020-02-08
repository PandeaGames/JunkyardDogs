using JunkyardDogs.Specifications;
using Chassis = JunkyardDogs.Components.Chassis;
using Weapon = JunkyardDogs.Specifications.Weapon;

namespace JunkyardDogs.Simulation
{
    public class SimMeleeAttack : SimPhysicalAttackObject
    {
        public Melee Melee;
        public SimMeleeAttack(SimulatedEngagement engagement, SimBot simBot, Chassis.ArmamentLocation armementLocation) : base(engagement, simBot, armementLocation)
        {
            Melee = simBot.bot.GetArmament(armementLocation).GetSpec() as Melee;
            colliders.Add(CreateCollider(Melee));
            body.isTrigger = true;
            body.rotation = simBot.body.rotation;
        }

        public override void OnSimEvent(SimulatedEngagement engagement, SimPostLogicEvent simEvent)
        {
            base.OnSimEvent(engagement, simEvent);
            engagement.MarkForRemoval(this);
        }
    }
}