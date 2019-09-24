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
            public int numberOfPreviousConcurrentDecisions;
            public int maxNumberOfTicksForMovement;
            public bool shouldContinueMoving;
            public int evasiveness;
            public bool arenaOverride;
            public bool oppositeSideArenaOverride;
            public bool botInitiatorOverride;
        }
        
        private StrafeDirection _direction;
        public StrafeDirection Direction
        {
            get { return _direction; }
        }
        
        private const int maxNumberOfTicksForMovement = 30;
            
        public DecisionStrafe(StrafeDirection direction)
        {
            _direction = direction;
        }

        public Logic GetDecisionWeight(SimBot simBot, SimulatedEngagement engagement)
        {
            DecisionStrafeLogic logic = new DecisionStrafeLogic();

            logic.evasiveness = simBot.bot.GetCPUAttribute(CPU.CPUAttribute.Evasiveness);
            logic.maxNumberOfTicksForMovement = maxNumberOfTicksForMovement;

            if (_direction == StrafeDirection.Left)
            {
                logic.numberOfPreviousConcurrentDecisions =
                    simBot.ConcurrentDecisionsOfType<DecisionMoveLeft>();
                logic.arenaOverride = simBot.IsAgainstArena(SimBot.Side.Left);
                logic.oppositeSideArenaOverride = simBot.IsAgainstArena(SimBot.Side.Right);
                logic.botInitiatorOverride = simBot.Initiator == Initiator.RED;
            }
            else
            {
                logic.numberOfPreviousConcurrentDecisions =
                    simBot.ConcurrentDecisionsOfType<DecisionMoveRight>();
                logic.arenaOverride = simBot.IsAgainstArena(SimBot.Side.Right);
                logic.oppositeSideArenaOverride = simBot.IsAgainstArena(SimBot.Side.Left);
                logic.botInitiatorOverride = simBot.Initiator == Initiator.BLUE;
            }

            logic.shouldContinueMoving = logic.numberOfPreviousConcurrentDecisions > 0 &&
                                                  logic.numberOfPreviousConcurrentDecisions <
                                                  logic.maxNumberOfTicksForMovement;

            if (logic.arenaOverride 
                || _direction == StrafeDirection.Left && simBot.StrafeDirection == StrafeDirection.Right
                || _direction == StrafeDirection.Right && simBot.StrafeDirection == StrafeDirection.Left)
            {
                logic.priority = (int) DecisionPriority.None;
            }
            else if (logic.shouldContinueMoving)
            {
                logic.priority = (int) DecisionPriority.Movement;
            }
            else if(logic.oppositeSideArenaOverride)
            {
                logic.weight = logic.evasiveness * 2;
            }
            else
            {
                logic.weight = logic.evasiveness;
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