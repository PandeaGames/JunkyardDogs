using UnityEngine;
using System;
using JunkyardDogs.Simulation.Agent;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;
using System.Collections.Generic;
using Agent = JunkyardDogs.Simulation.Agent.Agent;

[Serializable]
public class AgentBlueprint : Blueprint<Agent, AgentBlueprintData>
{
    [Serializable]
    public class AgentStateBlueprint
    {
        [SerializeField, WeakReference(typeof(JunkyardDogs.Simulation.Behavior.Action))]
        private List<WeakReference> _directiveActions;
        
        [SerializeField, WeakReference(typeof(JunkyardDogs.Simulation.Knowledge.State))]
        private WeakReference _state;
        
        public List<WeakReference> DirectiveActions
        {
            get { return _directiveActions; }
        }
        
        public WeakReference State
        {
            get { return _state; }
        }
    }

    [SerializeField]
    private List<AgentStateBlueprint> _states;
    
    protected override void DoGenerate(int seed, Action<Agent> onComplete, Action onError)
    {
        Agent agent = new Agent();
        
        _states.ForEach((stateBlueprint) =>
        {
            AgentState state = new AgentState();
            state.StateWeakReference = stateBlueprint.State;
            agent.States.Add(state);
            
            stateBlueprint.DirectiveActions.ForEach((directionAction) =>
            {
                Directive directive = new Directive();
                directive.ActionWeakReference = directionAction;
                state.Directives.Add(directive);
            });
        });
        
        TaskProvider.Instance.DelayedAction(() => onComplete(agent));
    }
}