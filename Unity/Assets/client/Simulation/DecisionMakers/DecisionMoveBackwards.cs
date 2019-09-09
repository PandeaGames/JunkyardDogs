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

            logic.cautiousness = simBot.bot.GetCPUAttribute(CPU.CPUAttribute.Cautiousness);
            
            logic.clampedDistance = clampedDistance;
            logic.clampedDistancedForProximity = distanceProximityMultiplier;
            logic.numberOfPreviousBackwardDecisionsToCheck = numberOfPreviousBackwardDecisionsToCheck;
            logic.numberOfPreviousBackwardDecisionsCap = logic.cautiousness;  
            
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

            bool isMovementCapped =
                logic.numberOfPreviousBackwardDecisions > logic.numberOfPreviousBackwardDecisionsCap;

            if (isMovementCapped)
            {
                logic.priority = (int) DecisionPriority.None;
            }
            else
            {
                bool hasBeenMovingBackward = logic.numberOfPreviousConcurrentBackwardDecisions > 0;
                logic.weight = (int) (logic.cautiousness * logic.proximityMultiplier) + (hasBeenMovingBackward ? 2500 : 0);
                
                /*if (hasBeenMovingBackward)
                {
                    logic.weight = (int) HardDecisions.Movement;
                }
                else
                {
                    logic.weight = (int) HardDecisions.Normal;
                    
                }*/
            }

            return logic;
        }

        public void MakeDecision(SimBot simBot, SimulatedEngagement engagement)
        {
            Vector2 vector = new Vector2(0, -simBot.bot.Chassis.Engine.Acceleration);
            simBot.body.accelerationPerSecond = vector;
            simBot.body.rotation.SetFromToRotation( simBot.body.position, simBot.opponent.body.position );
            DecisionStrafe.ClampSpeed(simBot.body, simBot.bot.Chassis.Engine.MaxSpeed);  
            engagement.SendEvent(new MoveDecisionEvent(simBot, vector));
        }
    }
}