using JunkyardDogs.Specifications;
using UnityEngine;
using UnityEngine.Scripting;

namespace JunkyardDogs.Simulation
{
    [Preserve]
    public class DecisionMoveForward : AbstractDecisionMove
    {
        public class DecisionMoveForwardLogic : DecisionMoveLogic
        {
            public int aggressiveness;
            public int targetDistance;
            public int distance;
            public int numberOfPreviousConcurrentBackwardDecisions;
            public int maxNumberOfTicksForMovement;
            public bool shouldContinueMoving;
        }
        
        private const int maxNumberOfTicksForMovement = 20;
        
        public override Logic GetDecisionWeight(SimBot simBot, SimulatedEngagement engagement)
        {
            DecisionMoveForwardLogic logic = new DecisionMoveForwardLogic();
            logic.plane = DecisionPlane.Base;

            logic.aggressiveness = simBot.bot.GetCPUAttribute(CPU.CPUAttribute.Aggressiveness);
            logic.distance = (int) Vector2.Distance(simBot.body.position, simBot.opponent.body.position);
            logic.targetDistance = (10 - logic.aggressiveness) + 1;
            logic.maxNumberOfTicksForMovement = maxNumberOfTicksForMovement;
            
            logic.numberOfPreviousConcurrentBackwardDecisions =
                simBot.ConcurrentDecisionsOfType<DecisionMoveForward>(logic.plane);

            logic.shouldContinueMoving = logic.numberOfPreviousConcurrentBackwardDecisions > 0 &&
                                                  logic.distance > logic.targetDistance &&
                                                  logic.numberOfPreviousConcurrentBackwardDecisions <
                                                  logic.maxNumberOfTicksForMovement;

            if (logic.shouldContinueMoving)
            {
                logic.priority = DecisionPriority.Movement;
            }
            else if (logic.targetDistance < logic.distance)
            {
                logic.weight = logic.aggressiveness * 2;
            }
            else
            {
                logic.priority = DecisionPriority.None;
                //logic.weight = logic.aggressiveness;
            }
            
            /*
            logic.clampedDistance = clampedDistance;
            logic.distanceProximityMultiplier = distanceProximityMultiplier;
            logic.numberOfPreviousForwardDecisionsToCheck = numberOfPreviousForwardDecisionsToCheck;
            logic.numberOfPreviousForwardDecisionsCap = numberOfPreviousForwardDecisionsCap;
            
            logic.aggressiveness = simBot.bot.GetCPUAttribute(CPU.CPUAttribute.Aggressiveness);
            logic.distance = Vector2.Distance(simBot.body.position, simBot.opponent.body.position);
            logic.clampedDistancedForProximity = Mathf.Min(logic.distance, logic.clampedDistance);
            logic.desiredDistance = 2 + (20 * (1 - (float) logic.aggressiveness / 100));
            logic.distanceDelta = logic.distance - logic.desiredDistance;
            
            logic.proximityMultiplier = 1 + logic.distanceDelta * logic.distanceProximityMultiplier;

            //calculate event multiplier
            logic.numberOfPreviousForwardDecisions =
                simBot.CountLastDecisionsOfType<DecisionMoveForward>(logic.numberOfPreviousForwardDecisionsToCheck);
            logic.numberOfPreviousConcurrentForwardDecisions =
                simBot.ConcurrentDecisionsOfType<DecisionMoveForward>();
            

            bool isMovementCapped =
                logic.numberOfPreviousForwardDecisions > logic.numberOfPreviousForwardDecisionsCap;

            if (isMovementCapped)
            {
                logic.priority = (int) DecisionPriority.None;
            }
            else
            {
                //logic.priority = (int) DecisionPriority.Normal;
                bool hasBeenMovingForward = logic.numberOfPreviousConcurrentForwardDecisions > 0;
                if (hasBeenMovingForward)
                {
                    logic.weight = 2500;
                }
                else
                {
                    logic.weight = (int)(logic.proximityMultiplier * logic.distanceDelta) * 30;
                }

               /* if (hasBeenMovingForward)
                {
                    logic.weight = (int) HardDecisions.Movement;
                }
                else
                {
                    logic.weight = (int) (logic.aggressiveness * logic.proximityMultiplier);
                }
            }*/

            return logic;
        }

        public override void MakeDecision(SimBot simBot, SimulatedEngagement engagement)
        {
            Vector2 vector = new Vector2(0, simBot.bot.Chassis.Engine.ForwardAcceleration);
            simBot.body.accelerationPerSecond = vector;
            simBot.body.rotation.SetFromToRotation( simBot.body.position, simBot.opponent.body.position );
            DecisionStrafe.ClampSpeed(simBot.body, simBot.bot.Chassis.Engine.ForwardMaxSpeed);  
            engagement.SendEvent(new MoveDecisionEvent(simBot, vector));
        }
    }
}