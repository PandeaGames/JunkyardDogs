using JunkyardDogs.Specifications;
using Weapon = JunkyardDogs.Components.Weapon;

namespace JunkyardDogs.Simulation
{
    public class DecisionWeaponHold : IDecisionMaker
    {
        public class DecisionWeaponHoldLogic : Logic
        {
            public bool wasLastWeaponHitscan;
            public double timeOfFire;
            public double lengthOfHitscanShot;
            public double shotEndTime;
            public bool isShotComplete;
        }

        public Logic GetDecisionWeight(SimBot simBot, SimulatedEngagement engagement)
        {
            DecisionWeaponHoldLogic logic = new DecisionWeaponHoldLogic();
            logic.plane = DecisionPlane.Base;
            logic.priority = DecisionPriority.None;
            SimBotDecisionPlane.WeightedDecision lastWeaponFire = simBot.GetLastWeightedDecisionOfType<DecisionWeaponFire>(logic.plane);

            if (lastWeaponFire != null)
            {
                logic.plane = lastWeaponFire.logic.plane;
                DecisionWeaponFire decisionWeaponFire = lastWeaponFire.DecisionMaker as DecisionWeaponFire;
                Specifications.Specification spec = simBot.bot.GetArmament(decisionWeaponFire.armamentLocation)
                    .GetSpec();
                Hitscan hitscan = spec is Hitscan ? (Hitscan) spec:null;
                logic.wasLastWeaponHitscan = hitscan != null;
                if (logic.wasLastWeaponHitscan)
                {
                    logic.timeOfFire = engagement.ConvertStepsToSeconds(lastWeaponFire.simulationTick);
                    logic.lengthOfHitscanShot = hitscan.ShotTime;
                    logic.shotEndTime = logic.timeOfFire + logic.lengthOfHitscanShot;
                    logic.isShotComplete = logic.shotEndTime < engagement.CurrentSeconds;

                    if (!logic.isShotComplete)
                    {
                        logic.priority = DecisionPriority.FireWeapon;
                    }
                }
            }
            
            return logic;
        }

        public void MakeDecision(SimBot simBot, SimulatedEngagement engagement)
        {
            //do nothing. we are shooting weapon
        }
    }
}