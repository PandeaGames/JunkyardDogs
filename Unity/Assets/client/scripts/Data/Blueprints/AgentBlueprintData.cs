using System;
using System.Collections.Generic;
using JunkyardDogs.Simulation.Agent;
using UnityEngine;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

[CreateAssetMenu(menuName = "Blueprints/Agent Blueprint")]
public class AgentBlueprintData : BlueprintData<Agent>
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
    
    public override Agent DoGenerate(int seed)
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

        return agent;
    }
}