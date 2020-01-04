using System;
using JunkyardDogs.Specifications;
using UnityEngine;
using UnityEngine.Scripting;

namespace JunkyardDogs.Simulation
{
    public class DecisionMoveBackwardsLogic : Logic
    {
        public int cautiousness;
        public int targetDistance;
        public int distance;
        public int numberOfPreviousConcurrentBackwardDecisions;
        public int maxNumberOfTicksForMovement;
        public bool shouldContinueMovingBackwards;
    }
    
    [Preserve]
    public class DecisionMoveBackwards : AbstractDecisionMove
    {
        private const int maxNumberOfTicksForMovement = 20;
        
        public override Logic GetDecisionWeight(SimBot simBot, SimulatedEngagement engagement)
        {
            DecisionMoveBackwardsLogic logic = new DecisionMoveBackwardsLogic();
            logic.plane = DecisionPlane.Base;

            logic.cautiousness = simBot.bot.GetCPUAttribute(CPU.CPUAttribute.Cautiousness);
            logic.distance = (int) Vector2.Distance(simBot.body.position, simBot.opponent.body.position);
            logic.targetDistance = logic.cautiousness;
            logic.maxNumberOfTicksForMovement = maxNumberOfTicksForMovement;
            
            logic.numberOfPreviousConcurrentBackwardDecisions =
                simBot.ConcurrentDecisionsOfType<DecisionMoveBackwards>(logic.plane);

            logic.shouldContinueMovingBackwards = logic.numberOfPreviousConcurrentBackwardDecisions > 0 &&
                                                  logic.numberOfPreviousConcurrentBackwardDecisions <
                                                  logic.maxNumberOfTicksForMovement;

            if (logic.shouldContinueMovingBackwards)
            {
                logic.priority = DecisionPriority.Movement;
            }
            else if (logic.targetDistance < logic.distance)
            {
                logic.weight = logic.cautiousness;
            }
            else
            {
                logic.weight = logic.cautiousness * 2;
            }
            
            /*logic.clampedDistance = clampedDistance;
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
                    
                }
            }*/

            return logic;
        }

        public override void MakeDecision(SimBot simBot, SimulatedEngagement engagement)
        {
            Vector2 vector = new Vector2(0, -simBot.bot.Chassis.Engine.BackwardAcceleration);
            simBot.body.accelerationPerSecond = vector;
            simBot.body.rotation.SetFromToRotation( simBot.body.position, simBot.opponent.body.position );
            DecisionStrafe.ClampSpeed(simBot.body, simBot.bot.Chassis.Engine.BackwardMaxSpeed);  
            engagement.SendEvent(new MoveDecisionEvent(simBot, vector));
        }
    }
}