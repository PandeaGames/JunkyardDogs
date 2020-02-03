using JunkyardDogs.Simulation.Simulation;
using JunkyardDogs.Specifications;
using UnityEngine;
using UnityEngine.Scripting;

namespace JunkyardDogs.Simulation
{
    public enum StrafeDirection
    {
        Left, 
        Right
    }
    
    [Preserve]
    public abstract class DecisionStrafe : AbstractDecisionMove
    {
        public class DecisionStrafeLogic : DecisionMoveLogic
        {
            public int numberOfPreviousConcurrentDecisions;
            public int maxNumberOfTicksForStrafeMovement;
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
        
        private const int maxNumberOfTicksForMovement = 15;
            
        public DecisionStrafe(StrafeDirection direction)
        {
            _direction = direction;
        }

        public override Logic GetDecisionWeight(SimBot simBot, SimulatedEngagement engagement)
        {
            DecisionStrafeLogic logic = new DecisionStrafeLogic();
            logic.plane = DecisionPlane.Base;

            logic.evasiveness = simBot.bot.GetCPUAttribute(CPU.CPUAttribute.Evasiveness);
            logic.maxNumberOfTicksForStrafeMovement = maxNumberOfTicksForMovement;
            logic.maxNumberOfTicksForMovement = MAX_NUMBER_OF_MOVEMENT_TICKS;
            logic.numberOfPreviousConcurrentMovementDecisions = simBot.ConcurrentDecisionsOfType<AbstractDecisionMove>(logic.plane);
            logic.shouldStopMoving =
                logic.numberOfPreviousConcurrentMovementDecisions > logic.maxNumberOfTicksForMovement;

            if (_direction == StrafeDirection.Left)
            {
                logic.numberOfPreviousConcurrentDecisions =
                    simBot.ConcurrentDecisionsOfType<DecisionMoveLeft>(logic.plane);
                logic.arenaOverride = simBot.IsAgainstArena(SimBot.Side.Left);
                logic.oppositeSideArenaOverride = simBot.IsAgainstArena(SimBot.Side.Right);
                logic.botInitiatorOverride = simBot.Initiator == Initiator.RED;
            }
            else
            {
                logic.numberOfPreviousConcurrentDecisions =
                    simBot.ConcurrentDecisionsOfType<DecisionMoveRight>(logic.plane);
                logic.arenaOverride = simBot.IsAgainstArena(SimBot.Side.Right);
                logic.oppositeSideArenaOverride = simBot.IsAgainstArena(SimBot.Side.Left);
                logic.botInitiatorOverride = simBot.Initiator == Initiator.BLUE;
            }

            logic.shouldContinueMoving = logic.numberOfPreviousConcurrentDecisions > 0 &&
                                                  logic.numberOfPreviousConcurrentDecisions <
                                                  logic.maxNumberOfTicksForStrafeMovement;

            if (logic.arenaOverride 
                || _direction == StrafeDirection.Left && simBot.StrafeDirection == StrafeDirection.Right
                || _direction == StrafeDirection.Right && simBot.StrafeDirection == StrafeDirection.Left
                || logic.shouldStopMoving)
            {
                logic.priority = DecisionPriority.None;
            }
            else if (logic.shouldContinueMoving)
            {
                logic.priority = DecisionPriority.Movement;
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

        public override void MakeDecision(SimBot simBot, SimulatedEngagement engagement)
        {
            //apply velocity
            Vector2 vector = new Vector2(_direction == StrafeDirection.Left ? -simBot.bot.Chassis.Engine.StrafeAcceleration:simBot.bot.Chassis.Engine.StrafeAcceleration, 0);
            simBot.body.accelerationPerSecond = vector;
            
            simBot.body.rotation.SetFromToRotation( simBot.body.position, simBot.opponent.body.position);
            ClampSpeed(simBot.body, simBot.bot.Chassis.Engine.StrafeMaxSpeed);  
            
            engagement.SendEvent(new MoveDecisionEvent(simBot, vector));
        }

        public static void ClampSpeed(SimulatedBody body, float maxSpeed)
        {
            return;
            float magnitude = body.velocityPerSecond.magnitude;
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