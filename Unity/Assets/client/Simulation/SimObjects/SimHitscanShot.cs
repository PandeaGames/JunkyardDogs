using JunkyardDogs.Simulation.Simulation;
using JunkyardDogs.Specifications;
using UnityEngine;
using Chassis = JunkyardDogs.Components.Chassis;

namespace JunkyardDogs.Simulation
{
    public class SimHitscanShot : SimPhysicalAttackObject
    {
        private Hitscan hitscan;
        public SimHitscanShot(SimulatedEngagement engagement, SimBot simBot, Chassis.ArmamentLocation armementLocation) : base(engagement, simBot, armementLocation)
        {
            hitscan = simBot.bot.GetArmament(armementLocation).GetSpec<Hitscan>();
            colliders.Add(CreateCollider(hitscan));
            body.isTrigger = true;
            removeOnCollision = false;
        }
        
        public override void OnSimEvent(SimulatedEngagement engagement, SimPostLogicEvent simEvent)
        {
            base.OnSimEvent(engagement, simEvent);
            body.rotation.deg360 = simBot.body.rotation.deg360;
            double lengthOfHitscanShot = hitscan.ShotTime;
            double shotEndTime = instantiationTime + lengthOfHitscanShot;
            bool isShotComplete = shotEndTime < engagement.CurrentSeconds;

            if (isShotComplete)
            {
                engagement.MarkForRemoval(this);
            }

            SimulatedLineCollider collider = colliders[0] as SimulatedLineCollider; 
            collider.angle = body.rotation.rad;
            //collider.angle = 225f * Mathf.Deg2Rad;
            //collider.angle = 45f * Mathf.Deg2Rad;
            body.position = new Vector2(simBot.body.position.x, simBot.body.position.y);
        }
    }
}