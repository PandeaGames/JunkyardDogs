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
        public event Action OnSwapCPU;
        public event Action OnCPUSwapped;
        public event Action<int> OnChooseDirective;
        public event Action<int> OnDirectiveChosen;
        
        public Bot Bot;

        public void OnDoneClicked()
        {
            if (OnDone != null)
                OnDone();
        }

        public void SwapCPU()
        {
            if (OnSwapCPU != null)
            {
                OnSwapCPU();
            }
        }
        
        public void CPUSwapped()
        {
            if (OnCPUSwapped != null)
            {
                OnCPUSwapped();
            }
        }

        public void ChooseDirective(int index)
        {
            if (OnChooseDirective != null)
            {
                OnChooseDirective(index);
            }
        }

        public void DirectiveChosen(int index)
        {
            if (OnDirectiveChosen != null)
            {
                OnDirectiveChosen(index);
            }
        }
    }
}