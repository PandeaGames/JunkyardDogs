using JunkyardDogs.Specifications;
using UnityEngine;

namespace JunkyardDogs.Simulation
{
    public class DecisionMoveForwardLogic : Logic
    {
        public float clampedDistance;
        public float distanceProximityMultiplier;
        public int numberOfPreviousForwardDecisionsToCheck;
        public int numberOfPreviousForwardDecisionsCap;
        
        public float distance;
        public float clampedDistancedForProximity;
        public float proximityMultiplier;

        public int numberOfPreviousForwardDecisions;
        public int numberOfPreviousConcurrentForwardDecisions;

        public int aggressiveness;

        public int weight;
    }
    
    public class DecisionMoveForward : IDecisionMaker
    {
        private const float clampedDistance = 10;
        private const float distanceProximityMultiplier = 0.16f;
        private const int numberOfPreviousForwardDecisionsToCheck = 100;
        private const int numberOfPreviousForwardDecisionsCap = 20;
        
        public Logic GetDecisionWeight(SimBot simBot, SimulatedEngagement engagement)
        {
            DecisionMoveForwardLogic logic = new DecisionMoveForwardLogic();

            logic.clampedDistance = clampedDistance;
            logic.distanceProximityMultiplier = distanceProximityMultiplier;
            logic.numberOfPreviousForwardDecisionsToCheck = numberOfPreviousForwardDecisionsToCheck;
            logic.numberOfPreviousForwardDecisionsCap = numberOfPreviousForwardDecisionsCap;
            
            logic.distance = Vector2.Distance(simBot.position, simBot.opponent.position);
            logic.clampedDistancedForProximity = Mathf.Min(logic.distance, logic.clampedDistance);
            logic.proximityMultiplier = 1 + logic.clampedDistancedForProximity * logic.distanceProximityMultiplier;

            //calculate event multiplier
            logic.numberOfPreviousForwardDecisions =
                simBot.CountLastDecisionsOfType<DecisionMoveForward>(logic.numberOfPreviousForwardDecisionsToCheck);
            logic.numberOfPreviousConcurrentForwardDecisions =
                simBot.ConcurrentDecisionsOfType<DecisionMoveForward>();
            
            logic.aggressiveness = simBot.bot.GetCPUAttribute(CPU.CPUAttribute.Aggressiveness);

            bool isMovementCapped =
                logic.numberOfPreviousForwardDecisions > logic.numberOfPreviousForwardDecisionsCap;

            if (isMovementCapped)
            {
                logic.weight = -1;
            }
            else
            {
                bool hasBeenMovingForward = logic.numberOfPreviousConcurrentForwardDecisions > 0;

                if (hasBeenMovingForward)
                {
                    logic.weight = (int) HardDecisions.Movement;
                }
                else
                {
                    logic.weight = (int) (logic.aggressiveness * logic.proximityMultiplier);
                }
            }

            return logic;
        }

        public void MakeDecision(SimBot simBot, SimulatedEngagement engagement)
        {
            Vector2 vector = new Vector2(0, 1);
            simBot.velocityPerSecond = vector;
            engagement.SendEvent(new MoveDecisionEvent(simBot, vector));
        }
    }
}