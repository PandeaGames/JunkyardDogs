using JunkyardDogs.Behavior;
using JunkyardDogs.Simulation;
using PandeaGames.ViewModels;
using System;

namespace JunkyardDogs
{
    public class MatchViewModel : AbstractViewModel
    {
        public event System.Action OnMatchComplete;
        
        public Engagement Engagement;

        public void MatchComplete()
        {
            if (OnMatchComplete != null)
            {
                OnMatchComplete();
            }
        }
    }
}