using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JunkyardDogs.Components;
using UnityEngine.UI.Extensions;
using Weapon = JunkyardDogs.Specifications.Weapon;

namespace JunkyardDogs.Simulation
{
    public class SimBot : SimPhysicsObject, ISimulatedEngagementEventHandler
    {
        public SimBot(SimulatedEngagement engagement) : base(engagement)
        {
            
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

        public class WeightedDecision
        {
            public readonly Logic logic;
            public readonly int simulationTick;
            public int Weight
            {
                get { return logic.weight; }
            }
            public readonly IDecisionMaker DecisionMaker;
            
            public WeightedDecision(Logic logic, IDecisionMaker decisionMaker, int simulationTick)
            {
                this.logic = logic;
                DecisionMaker = decisionMaker;
                this.simulationTick = simulationTick;
            }
        }
        
        public Bot bot;
        public SimBot opponent;
        public double DamageTaken;
        public double StunStartTick = -1;
        public double StunLength;

        private List<WeightedDecision[]> weightedDecisionHistory = new List<WeightedDecision[]>();
        
        private List<WeightedDecision> _decisionsCache;
        public List<WeightedDecision> Decisions
        {
            get
            {
                if (_decisionsCache == null)
                {
                    _decisionsCache = new List<WeightedDecision>();
                }

                return _decisionsCache;
            }
        }
        
        private List<List<WeightedDecision>> _weightedDecisionsCache;
        public List<List<WeightedDecision>> WeightedDecisions
        {
            get
            {
                if (_weightedDecisionsCache == null)
                {
                    _weightedDecisionsCache = new List<List<WeightedDecision>>();
                }

                return _weightedDecisionsCache;
            }
        }

        public double RemainingHealth
        {
            get { return bot.TotalHealth - DamageTaken; }
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
            DamageTaken += attack.Damage;
            engagement.SendEvent(new SimDamageTakenEvent(this, attack.Damage));
        }

        public void OnSimEvent(SimulatedEngagement engagement, SimLogicEvent simEvent)
        {
            //MAKE DECISION
            WeightedDecision[] weightedDecisions = GetWeightedDecision();

            bool hasDecisions = weightedDecisions.Length > 0;
            
            if (hasDecisions)
            {
                weightedDecisionHistory.Add(weightedDecisions);
                WeightedDecision bestDecision = weightedDecisions[0];
            
                bestDecision.DecisionMaker.MakeDecision(this, engagement);
            
                Decisions.Add(bestDecision);
                WeightedDecisions.Add(new List<WeightedDecision>(weightedDecisions));
                engagement.SendEvent(new WeightedDecisionEvent(bestDecision));
            }
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
                if (a.Weight == b.Weight)
                {
                    return 0;
                }
                
                return a.Weight < b.Weight ? 1:-1;
            });

            return weightedDecisions.ToArray();
        }
        
        public bool IsLastDecisionOfType<TDecision>(Type[] typeFilters) where TDecision : IDecisionMaker
        {
            for (int i = Decisions.Count - 1; i >= 0; i--)
            {
                IDecisionMaker decision = Decisions[i].DecisionMaker;
                Type decisionType = decision.GetType();

                bool isTargetType = decision is TDecision;
                bool isFilteredType = false;

                foreach (Type filteredType in typeFilters)
                {
                    if (filteredType.IsAssignableFrom(decisionType))
                    {
                        isFilteredType = true;
                        break;
                    }
                }

                if (isTargetType)
                {
                    return true;
                }
                else if(!isFilteredType)
                {
                    break;
                }
            }

            return false;
        }

        public bool IsLastDecisionOfType<TDecision>() where TDecision : IDecisionMaker
        {
            int lastDecisionsOfType = CountLastDecisionsOfType<TDecision>(1);
            return lastDecisionsOfType > 0;
        }

        public int ConcurrentDecisionsOfType<TDecision>() where TDecision:IDecisionMaker
        {
            return ConcurrentDecisionsOfType<TDecision>(decision => { return true; });
        }
        
        public int ConcurrentDecisionsOfType<TDecision>(Func<TDecision, bool> predicate) where TDecision:IDecisionMaker
        {
            for (int i = Decisions.Count - 1; i >=0; i--)
            {
                IDecisionMaker decision = Decisions[i].DecisionMaker;

                bool predicateResult = true;

                if ((decision is TDecision))
                {
                    predicateResult = predicate.Invoke((TDecision) decision);
                }
                
                if (!(decision is TDecision) || !predicateResult)
                {
                    return (Decisions.Count - 1) - i;
                }
            }

            return 0;
        }
        
        public int CountLastDecisionsOfType<TDecision>() where TDecision:IDecisionMaker
        {
            return CountLastDecisionsOfType<TDecision>(Decisions.Count);
        }
        
        public int CountLastDecisionsOfType<TDecision>(int howManyDecisions) where TDecision:IDecisionMaker
        {
            return CountLastDecisionsOfType<TDecision>(howManyDecisions, decision => { return true; });
        }
        
        public int CountLastDecisionsOfType<TDecision>(int howManyDecisions, Func<TDecision, bool> predicate) where TDecision:IDecisionMaker
        {
            int count = 0;
            for (int i = Decisions.Count - 1; i >= Math.Max(1, Decisions.Count - howManyDecisions) - 1; i--)
            {
                IDecisionMaker decision = Decisions[i].DecisionMaker;
                if ((decision is TDecision) && predicate.Invoke((TDecision)decision))
                {
                    count++;
                }
            }

            return count;
        }

        public bool DecisionWasOfType<TDecision>(int howManyDecisions) where TDecision : IDecisionMaker
        {
            return DecisionWasOfType<TDecision>(howManyDecisions, decision => { return true; });
        }
        
        public bool DecisionWasOfType<TDecision>(int howManyDecisions, Func<TDecision, bool> predicate) where TDecision : IDecisionMaker
        {
            for (int i = Decisions.Count - 1; i >= Math.Max(1, Decisions.Count - howManyDecisions) - 1; i--)
            {
                IDecisionMaker decision = Decisions[i].DecisionMaker;
                if ((decision is TDecision) && predicate.Invoke((TDecision)decision))
                {
                    return true;
                }
            }

            return false;
        }
        
        public WeightedDecision GetLastWeightedDecisionOfType<TDecision>() where TDecision : IDecisionMaker
        {
            return GetLastWeightedDecisionOfType<TDecision>(Decisions.Count);
        }
        
        public WeightedDecision GetLastWeightedDecisionOfType<TDecision>(Func<WeightedDecision, bool> predicate) where TDecision : IDecisionMaker
        {
            return GetLastWeightedDecisionOfType<TDecision>(Decisions.Count, predicate);
        }
        
        public WeightedDecision GetLastWeightedDecisionOfType<TDecision>(int howManyDecisions) where TDecision : IDecisionMaker
        {
            return GetLastWeightedDecisionOfType<TDecision>(howManyDecisions, decision => { return true; });
        }
        
        public WeightedDecision GetLastWeightedDecisionOfType<TDecision>(int howManyDecisions, Func<WeightedDecision, bool> predicate) where TDecision : IDecisionMaker
        {
            for (int i = Decisions.Count - 1; i >= Math.Max(1, Decisions.Count - howManyDecisions) - 1; i--)
            {
                WeightedDecision decision = Decisions[i];
                if ((decision.DecisionMaker is TDecision) && predicate.Invoke(decision))
                {
                    return decision;
                }
            }

            return null;
        }
        
        public TDecision GetLastOfType<TDecision>() where TDecision : class, IDecisionMaker
        {
            return GetLastOfType<TDecision>(Decisions.Count);
        }
        
        public TDecision GetLastOfType<TDecision>(int howManyDecisions) where TDecision : class, IDecisionMaker
        {
            return GetLastWeightedDecisionOfType<TDecision>(howManyDecisions, decision => { return true; }).DecisionMaker as TDecision;
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
        
        public bool IsChargingWeapon(Chassis.ArmamentLocation position)
        {
            WeightedDecision decisionStartWeaponChargeWeightedDecision = GetLastWeightedDecisionOfType<DecisionStartWeaponCharge>();
            bool isLastDecisisonToStartWeaponCharge = IsLastDecisionOfType<DecisionStartWeaponCharge>();
            
            if (decisionStartWeaponChargeWeightedDecision != null)
            {
                DecisionStartWeaponCharge decisionStartWeaponCharge =
                    decisionStartWeaponChargeWeightedDecision.DecisionMaker as DecisionStartWeaponCharge;

                if (decisionStartWeaponCharge.armamentLocation == position)
                {
                    int simulationTicksSinceWeaponChargeStart =
                        (engagement.CurrentStep - 1) - decisionStartWeaponChargeWeightedDecision.simulationTick;
                    int numberOfChargeDecisionsSinceStartedCharging = CountLastDecisionsOfType<DecisionWeaponCharge>(simulationTicksSinceWeaponChargeStart);
                    bool hasChargingBeenInterrupted =
                        simulationTicksSinceWeaponChargeStart > numberOfChargeDecisionsSinceStartedCharging;
                
                    double timeOfStartCharge =
                        engagement.ConvertStepsToSeconds(decisionStartWeaponChargeWeightedDecision.simulationTick);
                    double timeOfChargeComplete =
                        timeOfStartCharge + decisionStartWeaponCharge.GetWeapon(this).GetSpec<Weapon>().ChargeTime;

                    bool isCharging = timeOfChargeComplete > engagement.CurrentSeconds;
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
        
        public bool IsInWeaponCooldown()
        {
            WeightedDecision decisionWeaponFireWeightedDecision = GetLastWeightedDecisionOfType<DecisionWeaponFire>();

            if (decisionWeaponFireWeightedDecision != null)
            {
                DecisionWeaponFire decisionWeaponFire =
                    decisionWeaponFireWeightedDecision.DecisionMaker as DecisionWeaponFire;

                double timeOfLastWeaponFire =
                    engagement.ConvertStepsToSeconds(decisionWeaponFireWeightedDecision.simulationTick);
                double timeOfCooldownComplete =
                    timeOfLastWeaponFire + decisionWeaponFire.GetWeapon(this).GetSpec<Weapon>().Cooldown;

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