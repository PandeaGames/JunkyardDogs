
using System;
using System.Collections.Generic;
using JunkyardDogs.Simulation;

public class SimBotDecisionPlane
{
    public class WeightedDecision
    {
        public readonly Logic logic;
        public readonly int simulationTick;
        public int Priority
        {
            get { return (int)logic.priority; }
        }
        public int Plane
        {
            get { return (int)logic.plane; }
        }
        public readonly IDecisionMaker DecisionMaker;
            
        public WeightedDecision(Logic logic, IDecisionMaker decisionMaker, int simulationTick)
        {
            this.logic = logic;
            DecisionMaker = decisionMaker;
            this.simulationTick = simulationTick;
        }
    }
    
    public List<WeightedDecision[]> weightedDecisionHistory = new List<WeightedDecision[]>();
    
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
                else if (i == 0)
                {
                    return Decisions.Count;
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
        
        public int TicksSinceLastDecisionOfType<TDecision>() where TDecision : class, IDecisionMaker
        {
            int count = Decisions.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                if (Decisions[i].DecisionMaker is TDecision)
                {
                    return count - i;
                }
            }
            
            return count;
        }
        
        public TDecision GetLastOfType<TDecision>(int howManyDecisions) where TDecision : class, IDecisionMaker
        {
            return GetLastWeightedDecisionOfType<TDecision>(howManyDecisions, decision => { return true; }).DecisionMaker as TDecision;
        }
}
