using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using JunkyardDogs.Components;
using JunkyardDogs.Simulation.Simulation;
using UnityEngine;
using UnityEngine.UI.Extensions;
using Random = System.Random;
using Weapon = JunkyardDogs.Specifications.Weapon;
using WeightedDecision = SimBotDecisionPlane.WeightedDecision;

namespace JunkyardDogs.Simulation
{
    public class SimBot : SimPhysicsObject, ISimulatedEngagementEventHandler
    {
        public enum Side
        {
            Top, 
            Bottom, 
            Left, 
            Right
        }

        private Dictionary<Side, SimBotArenaTrigger> _sideTriggers;
        public Initiator Initiator { private set; get; }
        public StrafeDirection StrafeDirection { private set; get; }
        
        public SimBot(SimulatedEngagement engagement, Initiator initiator) : base(engagement)
        {
            this.Initiator = initiator;
            SimulatedCircleCollider collider = new SimulatedCircleCollider(body);
            collider.radius = 0.8f;
            colliders.Add(collider);
            
            _sideTriggers = new Dictionary<Side, SimBotArenaTrigger>();
            
            _sideTriggers[Side.Top] = new SimBotArenaTrigger(engagement);
            _sideTriggers[Side.Bottom] = new SimBotArenaTrigger(engagement);
            _sideTriggers[Side.Left] = new SimBotArenaTrigger(engagement);
            _sideTriggers[Side.Right] = new SimBotArenaTrigger(engagement);
            
            engagement.Add(_sideTriggers[Side.Top]);
            engagement.Add(_sideTriggers[Side.Bottom]);
            engagement.Add(_sideTriggers[Side.Left]);
            engagement.Add(_sideTriggers[Side.Right]);
        }

        public bool IsAgainstArena(Side side)
        {
            return _sideTriggers[side].isActive;
        }
        
        private static IDecisionMaker[] DecisionMakersCache;
        public static IDecisionMaker[] DecisionMakers
        {
            get
            {
                if (DecisionMakersCache == null)
                {
                    List<IDecisionMaker> decisionMakersList = new List<IDecisionMaker>();
                    
                    Type type = typeof(IDecisionMaker);

                    Assembly[] asemblies = AppDomain.CurrentDomain.GetAssemblies();
                    List<Type> foundDecisionMakerTypes = new List<Type>();
                    
                    foreach (Assembly assembly in asemblies)
                    {
                        Type[] typesInAssembly = assembly.GetTypes();

                        foreach (Type typeInAssembly in typesInAssembly)
                        {
                            bool IsAssignableFrom = type.IsAssignableFrom(typeInAssembly);
                            bool isDecisionMaker = IsAssignableFrom;
                            isDecisionMaker &= typeInAssembly.IsClass;
                            isDecisionMaker &= !typeInAssembly.IsAbstract;

                            if (isDecisionMaker)
                            {
                                foundDecisionMakerTypes.Add(typeInAssembly);
                            }
                        }
                    }
                    
                    foreach (Type foundType in foundDecisionMakerTypes)
                    {
                        IDecisionMaker handlerInstance = (IDecisionMaker)Activator.CreateInstance(foundType);
                        decisionMakersList.Add(handlerInstance);
                    }

                    DecisionMakersCache = decisionMakersList.ToArray();
                }
                
                return DecisionMakersCache;
            }
        }

        public struct BodyState
        {
            public readonly Vector2 Position;
            public readonly Vector2 Velocity;
            public readonly float Rotation;

            public BodyState(Vector2 Position, Vector2 Velocity, float Rotation)
            {
                this.Position = Position;
                this.Velocity = Velocity;
                this.Rotation = Rotation;
            }
        }
        
        public Bot bot;
        public SimBot opponent;
        public double DamageTaken;
        public double StunStartTick = -1;
        public double StunLength;

        public Dictionary<DecisionPlane, SimBotDecisionPlane> decisionPlanes =
            new Dictionary<DecisionPlane, SimBotDecisionPlane>();
        public List<BodyState> BodyStates = new List<BodyState>();

        public SimBotDecisionPlane GetDecisionPlane(DecisionPlane plane)
        {
            SimBotDecisionPlane decisionPlane = null;

            if (!decisionPlanes.TryGetValue(plane, out decisionPlane))
            {
                decisionPlane = new SimBotDecisionPlane();
                decisionPlanes.Add(plane, decisionPlane);
            }
            
            return decisionPlane;
        }
        
        public double RemainingHealth
        {
            get { return bot.TotalHealth - DamageTaken; }
        }
        
        public double TotalHealth
        {
            get { return bot.TotalHealth; }
        }
        
        public Type[] EventsToHandle()
        {
            return new Type[] { typeof(SimLogicEvent), typeof(SimCollisionEvent) };
        }
        
        public void OnSimEvent(SimulatedEngagement engagement, SimEvent simEvent)
        {
            if (simEvent is SimLogicEvent)
            {
                OnSimEvent(engagement, simEvent as SimLogicEvent);
            }
        }

        public override void OnCollisionLoiter(SimPhysicsObject other)
        {
            base.OnCollisionLoiter(other);
            
            SimPhysicalAttackObject simPhysicalAttackObject = other as SimPhysicalAttackObject;

            if (simPhysicalAttackObject != null && simPhysicalAttackObject.SimBot == opponent)
            {
                HitByAttack(other as SimPhysicalAttackObject);
            }
        }

        public override void OnCollision(SimPhysicsObject other)
        {
            base.OnCollision(other);

            SimPhysicalAttackObject simPhysicalAttackObject = other as SimPhysicalAttackObject;

            if (simPhysicalAttackObject != null && simPhysicalAttackObject.SimBot == opponent)
            {
                HitByAttack(other as SimPhysicalAttackObject);
            }
        }

        private void HitByAttack(SimPhysicalAttackObject attack)
        {
            double damage = 0;
            if (attack is SimHitscanShot)
            {
                damage = attack.Damage * SimulatedEngagement.SimuationStep;
            }
            else
            {
                damage = attack.Damage;
            }
            
            DamageTaken += damage;
            engagement.SendEvent(new SimDamageTakenEvent(this, damage));

            Vector2 delta = body.position - attack.body.position;
            SimRotation directionalVelocityAngle = new SimRotation();
            directionalVelocityAngle.deg360 = attack.body.rotation.deg360 - body.rotation.deg360 + 90;
            float angle = directionalVelocityAngle.rad;
            float knockback = attack.Knockback;

            Vector2 knockbackVector = new Vector2(
                Mathf.Cos(angle) * knockback,
                Mathf.Sin(angle) * knockback
            );

            body.velocityPerSecond = new Vector2(knockbackVector.x + body.velocityPerSecond.x,
                knockbackVector.y + body.velocityPerSecond.y);
        }
        
        public void HitByAttack(float damage)
        {
            DamageTaken += damage;
            engagement.SendEvent(new SimDamageTakenEvent(this, damage));
        }

        private void PlaceArenaTrigger(float angleOffset, SimBotArenaTrigger trigger)
        {
            float angle = body.rotation.deg360 + angleOffset;

            float dx = Mathf.Cos(Mathf.Deg2Rad * angle) * 1;
            float dy = Mathf.Sin(Mathf.Deg2Rad * angle) * 1;
            
            trigger.body.position = new Vector2(body.position.x + dx, body.position.y + dy);
            trigger.body.rotation.deg360 =  body.rotation.deg360 + angleOffset;
        }

        public void OnSimEvent(SimulatedEngagement engagement, SimLogicEvent simEvent)
        {
            PlaceArenaTrigger(0, _sideTriggers[Side.Top]);
            PlaceArenaTrigger(180, _sideTriggers[Side.Bottom]);
            PlaceArenaTrigger(90, _sideTriggers[Side.Left]);
            PlaceArenaTrigger(-90, _sideTriggers[Side.Right]);

            if (StrafeDirection == StrafeDirection.Left && _sideTriggers[Side.Left].isActive)
            {
                StrafeDirection = StrafeDirection.Right;
            }
            else if (StrafeDirection == StrafeDirection.Right && _sideTriggers[Side.Right].isActive)
            {
                StrafeDirection = StrafeDirection.Left;
            }

            //MAKE DECISION
            SimBotDecisionPlane.WeightedDecision[] weightedDecisions = GetWeightedDecision();
            Dictionary<DecisionPlane, List<WeightedDecision>> planesOfDecisions = GetWeightedPlanes(weightedDecisions);

            foreach (KeyValuePair<DecisionPlane, List<WeightedDecision>> kvp in planesOfDecisions)
            {
                MakeDecision(kvp.Value.ToArray(), kvp.Key);
            }
        }

        private void MakeDecision(WeightedDecision[] weightedDecisions, DecisionPlane plane)
        {
            bool hasDecisions = weightedDecisions.Length > 0;
            body.accelerationPerSecond = Vector2.zero;

            SimBotDecisionPlane decisionPlane = decisionPlanes[plane];
            
            if (hasDecisions)
            {
                decisionPlane.weightedDecisionHistory.Add(weightedDecisions);
                WeightedDecision[] weightedDecisionsPlateau = FilterForHIghestWeightedDecisions(weightedDecisions);

                WeightedDecision bestDecision = PickRandomDecision(weightedDecisionsPlateau, engagement.Engagement.Seed);
            
                bestDecision.DecisionMaker.MakeDecision(this, engagement);
            
                decisionPlane.Decisions.Add(bestDecision);
                decisionPlane.WeightedDecisions.Add(new List<WeightedDecision>(weightedDecisions));
                engagement.SendEvent(new WeightedDecisionEvent(bestDecision));
                BodyStates.Add(new BodyState(
                    body.position,
                    body.accelerationPerSecond,
                    body.rotation.deg360
                ));
            }
        }

        private Dictionary<DecisionPlane, List<WeightedDecision>> GetWeightedPlanes(WeightedDecision[] decisions)
        {
            Dictionary<DecisionPlane, List<WeightedDecision>> planesOfDecisions = new Dictionary<DecisionPlane, List<WeightedDecision>>();

            foreach (WeightedDecision decision in decisions)
            {
                List<WeightedDecision> list = null;
                
                if (!planesOfDecisions.TryGetValue(decision.logic.plane, out list))
                {
                    list = new List<WeightedDecision>();
                    planesOfDecisions.Add(decision.logic.plane, list);
                }
                
                list.Add(decision);
            }

            return planesOfDecisions;
        }

        private WeightedDecision PickRandomDecision(WeightedDecision[] decisions, int seed)
        {
            WeightedDecision choice = null;
            int totalChangeWeight = 0;
            for (int i = 0; i < decisions.Length; i++)
            {
                totalChangeWeight += decisions[i].logic.weight;
            }

            UnityEngine.Random.State oldState = UnityEngine.Random.state;
            UnityEngine.Random.InitState(seed + engagement.CurrentStep);
            int pick = UnityEngine.Random.Range(0, totalChangeWeight);
            UnityEngine.Random.state = oldState;
            int totalChangeWeightForSearch = 0;
            
            for (int i = 0; i < decisions.Length; i++)
            {
                totalChangeWeightForSearch += decisions[i].logic.weight;

                if (totalChangeWeightForSearch > pick)
                {
                    choice = decisions[i];
                    break;
                }
            }

            return choice;
        }

        private WeightedDecision[] FilterForHIghestWeightedDecisions(WeightedDecision[] decisions)
        {
            List<WeightedDecision> filteredDecisions = new List<WeightedDecision>();

            int highestWeight = -1;

            for (int i = 0; i < decisions.Length; i++)
            {
                WeightedDecision weightedDecision = decisions[i];
                if (weightedDecision.Priority == highestWeight)
                {
                    filteredDecisions.Add(weightedDecision);
                }
                else if (weightedDecision.Priority > highestWeight)
                {
                    highestWeight = weightedDecision.Priority;
                    filteredDecisions.Clear();
                    filteredDecisions.Add(weightedDecision);
                }
            }

            return filteredDecisions.ToArray();
        }

        private WeightedDecision[] GetWeightedDecision()
        {
            List<WeightedDecision> weightedDecisions = new List<WeightedDecision>();

            foreach (IDecisionMaker decisionMaker in DecisionMakers)
            {
                Logic logic = decisionMaker.GetDecisionWeight(this, engagement);
                WeightedDecision weightedDecision = new WeightedDecision(logic, decisionMaker, engagement.CurrentStep);
                weightedDecisions.Add(weightedDecision);
            }
            
            weightedDecisions.Sort((a, b) => 
            {
                if (a.Priority == b.Priority)
                {
                    return 0;
                }
                
                return a.Priority < b.Priority ? 1:-1;
            });

            return weightedDecisions.ToArray();
        }
        
        public bool IsLastDecisionOfType<TDecision>(Type[] typeFilters, DecisionPlane plane) where TDecision : IDecisionMaker
        {
            return GetDecisionPlane(plane).IsLastDecisionOfType<TDecision>(typeFilters);
        }

        public bool IsLastDecisionOfType<TDecision>(DecisionPlane plane) where TDecision : IDecisionMaker
        {
            return GetDecisionPlane(plane).IsLastDecisionOfType<TDecision>();
        }

        public int ConcurrentDecisionsOfType<TDecision>(DecisionPlane plane) where TDecision:IDecisionMaker
        {
            return GetDecisionPlane(plane).ConcurrentDecisionsOfType<TDecision>();
        }
        
        public int ConcurrentDecisionsOfType<TDecision>(Func<TDecision, bool> predicate, DecisionPlane plane) where TDecision:IDecisionMaker
        {
            return GetDecisionPlane(plane).ConcurrentDecisionsOfType<TDecision>(predicate);
        }
        
        public int CountLastDecisionsOfType<TDecision>(DecisionPlane plane) where TDecision:IDecisionMaker
        {
            return GetDecisionPlane(plane).CountLastDecisionsOfType<TDecision>();
        }
        
        public int CountLastDecisionsOfType<TDecision>(int howManyDecisions, DecisionPlane plane) where TDecision:IDecisionMaker
        {
            return GetDecisionPlane(plane).CountLastDecisionsOfType<TDecision>(howManyDecisions);
        }
        
        public int CountLastDecisionsOfType<TDecision>(int howManyDecisions, Func<TDecision, bool> predicate, DecisionPlane plane) where TDecision:IDecisionMaker
        {
            return GetDecisionPlane(plane).CountLastDecisionsOfType<TDecision>(howManyDecisions, predicate);
        }

        public bool DecisionWasOfType<TDecision>(int howManyDecisions, DecisionPlane plane) where TDecision : IDecisionMaker
        {
            return GetDecisionPlane(plane).DecisionWasOfType<TDecision>(howManyDecisions);
        }
        
        public bool DecisionWasOfType<TDecision>(int howManyDecisions, Func<TDecision, bool> predicate, DecisionPlane plane) where TDecision : IDecisionMaker
        {
            return GetDecisionPlane(plane).DecisionWasOfType<TDecision>(howManyDecisions, predicate);
        }
        
        public WeightedDecision GetLastWeightedDecisionOfType<TDecision>(DecisionPlane plane) where TDecision : IDecisionMaker
        {
            return GetDecisionPlane(plane).GetLastWeightedDecisionOfType<TDecision>();
        }
        
        public WeightedDecision GetLastWeightedDecisionOfType<TDecision>(Func<WeightedDecision, bool> predicate, DecisionPlane plane) where TDecision : IDecisionMaker
        {
            return GetDecisionPlane(plane).GetLastWeightedDecisionOfType<TDecision>(predicate);
        }
        
        public WeightedDecision GetLastWeightedDecisionOfType<TDecision>(int howManyDecisions, DecisionPlane plane) where TDecision : IDecisionMaker
        {
            return GetDecisionPlane(plane).GetLastWeightedDecisionOfType<TDecision>(howManyDecisions);
        }
        
        public WeightedDecision GetLastWeightedDecisionOfType<TDecision>(int howManyDecisions, Func<WeightedDecision, bool> predicate, DecisionPlane plane) where TDecision : IDecisionMaker
        {
            return GetDecisionPlane(plane).GetLastWeightedDecisionOfType<TDecision>(howManyDecisions, predicate);
        }
        
        public TDecision GetLastOfType<TDecision>(DecisionPlane plane) where TDecision : class, IDecisionMaker
        {
            return GetDecisionPlane(plane).GetLastOfType<TDecision>();
        }
        
        public int TicksSinceLastDecisionOfType<TDecision>(DecisionPlane plane) where TDecision : class, IDecisionMaker
        {
            return GetDecisionPlane(plane).TicksSinceLastDecisionOfType<TDecision>();
        }
        
        public TDecision GetLastOfType<TDecision>(int howManyDecisions, DecisionPlane plane) where TDecision : class, IDecisionMaker
        {
            return GetDecisionPlane(plane).GetLastOfType<TDecision>(howManyDecisions);
        }

        public void Stun(double lengthOfStun)
        {
            StunStartTick = engagement.CurrentSeconds;
            StunLength = lengthOfStun;
        }

        public bool IsStunned()
        {
            if (StunStartTick != -1)
            {
                double endOfStun = StunStartTick + StunLength;
                bool isStunned = endOfStun > engagement.CurrentSeconds;
                return isStunned;
            }

            return false;
        }

        public void EndStun()
        {
            StunStartTick = -1;
            StunLength = 0;
        }
        
        public bool IsChargingWeapon(Chassis.ArmamentLocation position, DecisionPlane plane)
        {
            WeightedDecision decisionStartWeaponChargeWeightedDecision = GetLastWeightedDecisionOfType<DecisionStartWeaponCharge>(DecisionPlane.Base);
            bool isLastDecisisonToStartWeaponCharge = IsLastDecisionOfType<DecisionStartWeaponCharge>(DecisionPlane.Base);
            bool isLastDecisisonToWeaponCharge = IsLastDecisionOfType<DecisionWeaponCharge>(DecisionPlane.Base);

            
            if (decisionStartWeaponChargeWeightedDecision != null)
            {
                DecisionStartWeaponCharge decisionStartWeaponCharge =
                    decisionStartWeaponChargeWeightedDecision.DecisionMaker as DecisionStartWeaponCharge;

                if (decisionStartWeaponCharge.armamentLocation == position)
                {
                    int simulationTicksSinceWeaponChargeStart =
                        (engagement.CurrentStep - 1) - decisionStartWeaponChargeWeightedDecision.simulationTick;
                    int numberOfChargeDecisionsSinceStartedCharging = CountLastDecisionsOfType<DecisionWeaponCharge>(simulationTicksSinceWeaponChargeStart, plane);
                    bool hasChargingBeenInterrupted =
                        simulationTicksSinceWeaponChargeStart > numberOfChargeDecisionsSinceStartedCharging;
                
                    double timeOfStartCharge =
                        engagement.ConvertStepsToSeconds(decisionStartWeaponChargeWeightedDecision.simulationTick);
                    double timeOfChargeComplete =
                        timeOfStartCharge + decisionStartWeaponCharge.GetWeapon(this).GetSpec().ChargeTime;

                    bool isCharging = isLastDecisisonToWeaponCharge;
                    isCharging |= isLastDecisisonToStartWeaponCharge;
                    isCharging &= !hasChargingBeenInterrupted;
                    return isCharging;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }
        
        public bool IsInWeaponCooldown(DecisionPlane plane)
        {
            WeightedDecision decisionWeaponFireWeightedDecision = GetLastWeightedDecisionOfType<DecisionWeaponFire>(plane);

            if (decisionWeaponFireWeightedDecision != null)
            {
                DecisionWeaponFire decisionWeaponFire =
                    decisionWeaponFireWeightedDecision.DecisionMaker as DecisionWeaponFire;

                double timeOfLastWeaponFire =
                    engagement.ConvertStepsToSeconds(decisionWeaponFireWeightedDecision.simulationTick);
                double timeOfCooldownComplete =
                    timeOfLastWeaponFire + decisionWeaponFire.GetWeapon(this).GetSpec().Cooldown;

                bool isInCooldown = timeOfCooldownComplete > engagement.CurrentSeconds;
                return isInCooldown;
            }

            return false;
        }

        public void FireWeapon(Chassis.ArmamentLocation location)
        {
            //fire weapon
        }
    }
}