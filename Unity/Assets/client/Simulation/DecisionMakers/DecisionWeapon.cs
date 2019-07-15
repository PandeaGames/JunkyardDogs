using JunkyardDogs.Components;
using Substance.Game;

namespace JunkyardDogs.Simulation
{
    public abstract class DecisionWeapon : IDecisionMaker
    {
        protected Chassis.ArmamentLocation _armamentLocation;

        public Chassis.ArmamentLocation armamentLocation
        {
            get { return _armamentLocation; }
        }

        public DecisionWeapon(Chassis.ArmamentLocation armamentLocation)
        {
            _armamentLocation = armamentLocation;
        }

        public Logic GetDecisionWeight(SimBot simBot, SimulatedEngagement engagement)
        {
            Weapon weapon = GetWeapon(simBot);
            
            if (weapon != null)
            {
                return GetDecisionWeight(simBot, engagement, weapon);
            }
            else
            {
                Logic logic = new Logic();
                logic.weight = -1;
                return logic;
            }
        }

        public void MakeDecision(SimBot simBot, SimulatedEngagement engagement)
        {
            Weapon weapon = GetWeapon(simBot);

            if (weapon != null)
            {
                MakeDecision(simBot, engagement, weapon);
            }
        }

        protected abstract Logic GetDecisionWeight(SimBot simBot, SimulatedEngagement engagement, Weapon weapon);
        protected abstract void MakeDecision(SimBot simBot, SimulatedEngagement engagement, Weapon weapon);

        public Weapon GetWeapon(SimBot simBot)
        {
            WeaponProcessor processor = simBot.bot.Chassis.GetWeaponProcessor(_armamentLocation);;

            if (processor != null)
            {
                return processor.Weapon;
            }

            return null;
        }
    }
}