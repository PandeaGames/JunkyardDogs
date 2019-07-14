using JunkyardDogs.Specifications;
using UnityEngine;
using Chassis = JunkyardDogs.Components.Chassis;

namespace JunkyardDogs.Simulation
{
    public class SimMortarAttackObject : SimPhysicalAttackObject
    {
        private Mortar mortar;
        private Vector2 startPosition;
        private float targetDistance;
        private Vector2 targetPosition;
        public SimMortarAttackObject(SimulatedEngagement engagement, SimBot simBot, Chassis.ArmamentLocation armementLocation) : base(engagement, simBot, armementLocation)
        {
            mortar = simBot.bot.GetArmament(armementLocation).GetSpec<Mortar>();
            collider = CreateCollider(mortar);
            body.rotation = simBot.body.rotation;
            body.velocityPerSecond = new Vector2(0, mortar.Speed);
            body.doesCollide = false;
            startPosition = simBot.body.position;
            targetDistance = Vector2.Distance(simBot.body.position, simBot.opponent.body.position);
            targetPosition = simBot.opponent.body.position;
        }

        public override void OnSimEvent(SimulatedEngagement engagement, SimPostLogicEvent simEvent)
        {
            base.OnSimEvent(engagement, simEvent);

            if (body.doesCollide)
            {
                engagement.MarkForRemoval(this);
            }
            
            float distance = Vector2.Distance(simBot.body.position, simBot.opponent.body.position);

            if (distance > targetDistance && !body.doesCollide)
            {
                body.position = targetPosition;
                body.doesCollide = true;
            }
        }
    }
}