using System;
using JunkyardDogs.Specifications;
using UnityEngine;

namespace JunkyardDogs.Simulation
{
    public class DecisionMoveBackwardsLogic : Logic
    {
        //static values
        public float clampedDistance;
        public float distanceProximityMultiplier;
        public int numberOfPreviousBackwardDecisionsToCheck;
        public int numberOfPreviousBackwardDecisionsCap;
        
        //calculate proximity multiplier
        public float distance;
        public float clampedDistancedForProximity;
        public float invertedClampedDistancedForProximity;
        public float proximityMultiplier;

        //calculate event multiplier
        public int numberOfPreviousBackwardDecisions;
        public int numberOfPreviousConcurrentBackwardDecisions;

        public int cautiousness;
    }
    
    public class DecisionMoveBackwards : IDecisionMaker
    {
        private const float clampedDistance = 10;
        private const float distanceProximityMultiplier = 0.25f;
        private const int numberOfPreviousBackwardDecisionsToCheck = 100;
        private const int numberOfPreviousBackwardDecisionsCap = 20;
        
        public Logic GetDecisionWeight(SimBot simBot, SimulatedEngagement engagement)
        {
            DecisionMoveBackwardsLogic logic = new DecisionMoveBackwardsLogic();

            logic.clampedDistance = clampedDistance;
            logic.clampedDistancedForProximity = distanceProximityMultiplier;
            logic.numberOfPreviousBackwardDecisionsToCheck = numberOfPreviousBackwardDecisionsToCheck;
            logic.numberOfPreviousBackwardDecisionsCap = numberOfPreviousBackwardDecisionsCap;  
            
            //calculate proximity multiplier
            logic.distance = Vector2.Distance(simBot.body.position, simBot.opponent.body.position);
            logic.clampedDistancedForProximity = Math.Min(logic.distance, logic.clampedDistance);
            logic.invertedClampedDistancedForProximity = logic.clampedDistance - logic.clampedDistancedForProximity;
            logic.proximityMultiplier = 1 + logic.invertedClampedDistancedForProximity * logic.distanceProximityMultiplier;

            //calculate event multiplier
            logic.numberOfPreviousBackwardDecisions =
                simBot.CountLastDecisionsOfType<DecisionMoveBackwards>(logic.numberOfPreviousBackwardDecisionsToCheck);
            logic.numberOfPreviousConcurrentBackwardDecisions =
                simBot.ConcurrentDecisionsOfType<DecisionMoveBackwards>();
            
            logic.cautiousness = simBot.bot.GetCPUAttribute(CPU.CPUAttribute.Cautiousness);

            bool isMovementCapped =
                logic.numberOfPreviousBackwardDecisions > logic.numberOfPreviousBackwardDecisionsCap;

            if (isMovementCapped)
            {
                logic.weight = -1;
            }
            else
            {
                bool hasBeenMovingBackward = logic.numberOfPreviousConcurrentBackwardDecisions > 0;

                if (hasBeenMovingBackward)
                {
                    logic.weight = (int) HardDecisions.Movement;
                }
                else
                {
                    logic.weight = (int) (logic.cautiousness * logic.proximityMultiplier);
                }
            }

            return logic;
        }

        public void MakeDecision(SimBot simBot, SimulatedEngagement engagement)
        {
            Vector2 vector = new Vector2(0, -1);
            simBot.body.velocityPerSecond = vector;
            simBot.body.rotation.SetLookRotation(simBot.opponent.body.position);
            engagement.SendEvent(new MoveDecisionEvent(simBot, vector));
        }
    }
}