using System;
using System.Collections.Generic;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using JunkyardDogs.Simulation.Agent;
using UnityEngine;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

[CreateAssetMenu(menuName = "Blueprints/Agent Blueprint")]
public class AgentBlueprintData : BlueprintData<Agent>, IStaticDataBalance<AgentBlueprintBalanceObject>
{
    [SerializeField]
    private List<ActionStaticDataReference> _directiveActions;
        
    [SerializeField, StateKnowledgeStaticDataReference]
    private StateKnowledgeStaticDataReference _state;

    public override Agent DoGenerate(int seed)
    {
        Agent agent = new Agent();
        
        AgentState state = new AgentState();
        state.StateWeakReference = _state;
        agent.States.Add(state);
            
        _directiveActions.ForEach((directionAction) =>
        {
            ActionDirective actionDirective = new ActionDirective();
            actionDirective.ActionWeakReference = directionAction;
            state.Directives.Add(actionDirective);
        });

        return agent;
    }

    public void ApplyBalance(AgentBlueprintBalanceObject balance)
    {
        string[] directives = balance.directiveActions.Split(BalanceData.ListDelimiterChar);
        _directiveActions = new List<ActionStaticDataReference>();

        foreach (string actionId in directives)
        {
            ActionStaticDataReference actionReference = new ActionStaticDataReference();
            actionReference.ID = actionId;
            _directiveActions.Add(actionReference);
        }

        name = balance.name;
        _state = new StateKnowledgeStaticDataReference();
        _state.ID = balance.state;
    }

    public AgentBlueprintBalanceObject GetBalance()
    {
        AgentBlueprintBalanceObject balance = new AgentBlueprintBalanceObject();

        balance.state = _state == null ? string.Empty : _state.ID;
        balance.name = name;
        balance.directiveActions = string.Join(BalanceData.ListDelimiter, _directiveActions);

        return balance;
    }
}