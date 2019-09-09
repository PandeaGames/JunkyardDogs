using JunkyardDogs.Simulation.Simulation;
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

            logic.evasiveness = simBot.bot.GetCPUAttribute(CPU.CPUAttribute.Evasiveness);
            
            logic.numberOfPreviousStrafeDecisionsToCheck = numberOfPreviousStrafeDecisionsToCheck;
            logic.numberOfPreviousStrafeDecisionsCap = logic.evasiveness;

            //calculate event multiplier
            logic.numberOfPreviousStrafeDecisions =
                simBot.CountLastDecisionsOfType<DecisionStrafe>(logic.numberOfPreviousStrafeDecisionsToCheck,
                    (strafeDecision) => { return strafeDecision.Direction == _direction; });
            logic.numberOfPreviousConcurrentStrafeDecisions =
                simBot.ConcurrentDecisionsOfType<DecisionStrafe>(
                    (strafeDecision) => { return strafeDecision.Direction == _direction; });

            bool isStrafeMovementCapped =
                logic.numberOfPreviousStrafeDecisions > logic.numberOfPreviousStrafeDecisionsCap;

            if (isStrafeMovementCapped)
            {
                logic.priority = -1;
            }
            else
            {
                bool hasBeenStrafing = logic.numberOfPreviousConcurrentStrafeDecisions > 0;
                logic.weight = (int) logic.evasiveness + (hasBeenStrafing ? 2500 : 0);

               /* if (hasBeenStrafing)
                {
                    logic.weight = (int) HardDecisions.Movement;
                }
                else
                {
                    logic.weight = (int) (logic.evasiveness);
                }*/
            }

            return logic;
        }

        public void MakeDecision(SimBot simBot, SimulatedEngagement engagement)
        {
            //apply velocity
            Vector2 vector = new Vector2(_direction == StrafeDirection.Left ? -simBot.bot.Chassis.Engine.Acceleration:simBot.bot.Chassis.Engine.Acceleration, 0);
            simBot.body.accelerationPerSecond = vector;
            
            simBot.body.rotation.SetFromToRotation( simBot.body.position, simBot.opponent.body.position);
            ClampSpeed(simBot.body, simBot.bot.Chassis.Engine.MaxSpeed);  
            
            engagement.SendEvent(new MoveDecisionEvent(simBot, vector));
        }

        public static void ClampSpeed(SimulatedBody body, float maxSpeed)
        {
            float magnitude = body.velocityPerSecond.sqrMagnitude;
            if (magnitude > maxSpeed)
            {
                float modifier = magnitude / maxSpeed;
                Vector2 newSpeed  =new Vector2(
                    body.velocityPerSecond.x / modifier,
                    body.velocityPerSecond.y / modifier
                    );

                body.velocityPerSecond = newSpeed;
            }
        }
    }
}