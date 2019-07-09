using JunkyardDogs.Specifications;
using UnityEngine;

namespace JunkyardDogs.Simulation
{
    public enum StrafeDirection
    {
        Left, 
        Right
    }
    
    public abstract class DecisionStrafe : IDecisionMaker
    {
        public class DecisionStrafeLogic : Logic
        {
            public int numberOfPreviousStrafeDecisionsToCheck;
            public int numberOfPreviousStrafeDecisionsCap;
            
            public int numberOfPreviousStrafeDecisions;
            public int numberOfPreviousConcurrentStrafeDecisions;
            public int evasiveness;
            public int weight;
        }

        private const int numberOfPreviousStrafeDecisionsToCheck = 100;
        private const int numberOfPreviousStrafeDecisionsCap = 20;
        
        private StrafeDirection _direction;
        public StrafeDirection Direction
        {
            get { return _direction; }
        }
            
        public DecisionStrafe(StrafeDirection direction)
        {
            _direction = direction;
        }

        public Logic GetDecisionWeight(SimBot simBot, SimulatedEngagement engagement)
        {
            DecisionStrafeLogic logic = new DecisionStrafeLogic();

            logic.numberOfPreviousStrafeDecisionsToCheck = numberOfPreviousStrafeDecisionsToCheck;
            logic.numberOfPreviousStrafeDecisionsCap = numberOfPreviousStrafeDecisionsCap;

            //calculate event multiplier
            logic.numberOfPreviousStrafeDecisions =
                simBot.CountLastDecisionsOfType<DecisionStrafe>(logic.numberOfPreviousStrafeDecisionsToCheck,
                    (strafeDecision) => { return strafeDecision.Direction == _direction; });
            logic.numberOfPreviousConcurrentStrafeDecisions =
                simBot.ConcurrentDecisionsOfType<DecisionStrafe>(
                    (strafeDecision) => { return strafeDecision.Direction == _direction; });

            logic.evasiveness = simBot.bot.GetCPUAttribute(CPU.CPUAttribute.Evasiveness);

            bool isStrafeMovementCapped =
                logic.numberOfPreviousStrafeDecisions > logic.numberOfPreviousStrafeDecisionsCap;

            if (isStrafeMovementCapped)
            {
                logic.weight = -1;
            }
            else
            {
                bool hasBeenStrafing = logic.numberOfPreviousConcurrentStrafeDecisions > 0;

                if (hasBeenStrafing)
                {
                    logic.weight = (int) HardDecisions.Movement;
                }
                else
                {
                    logic.weight = (int) (logic.evasiveness);
                }
            }

            return logic;
        }

        public void MakeDecision(SimBot simBot, SimulatedEngagement engagement)
        {
            //apply velocity
            Vector2 vector = new Vector2(_direction == StrafeDirection.Left ? -1:1, 0);
            simBot.velocityPerSecond = vector;
            engagement.SendEvent(new MoveDecisionEvent(simBot, vector));
        }
    }
}