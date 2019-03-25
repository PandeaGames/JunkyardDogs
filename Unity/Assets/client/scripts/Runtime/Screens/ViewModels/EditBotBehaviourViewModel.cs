using JunkyardDogs.Behavior;
using JunkyardDogs.Components;
using PandeaGames.ViewModels;
using JunkyardDogs.Simulation.Agent;
using System;
using System.Collections.Generic;
using JunkyardDogs.Data;
using JunkyardDogs.Simulation.Behavior;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

namespace JunkyardDogs
{
    public class EditBotBehaviourViewModel : AbstractViewModel
    {
        public event System.Action OnSelectNewDirective;
        public event System.Action OnDone;
        public event System.Action<AgentState> OnSelectState; 
        public event System.Action<BehaviorAction, ActionStaticDataReference> OnActionAdded; 
        
        public Bot Bot;
        public WeakReference ActionList;
        
        public AgentState SelectedState;

        public void OnDoneClicked()
        {
            if (OnDone != null)
                OnDone();
        }
        
        public void OnSelectNewDirectiveClicked()
        {
            if (OnSelectNewDirective != null)
                OnSelectNewDirective();
        }
        
        public void SetSelectedState(int index)
        {
            List<AgentState> states = Bot.Agent.States;

            if (states.Count > index)
            {
                SelectedState =  states[index];
            }

            if (OnSelectState != null)
            {
                OnSelectState(SelectedState);
            }
        }
        
        public void AdddNewAction(ActionStaticDataReference action)
        {
            //TODO: Add action
            if (SelectedState != null && action != null)
            {
                Directive directive = new Directive();
                directive.ActionWeakReference = action;
                SelectedState.Directives.Add(directive);
                
                if (OnActionAdded != null)
                {
                    OnActionAdded(action.Data, action);
                }
            }
        }
    }
}