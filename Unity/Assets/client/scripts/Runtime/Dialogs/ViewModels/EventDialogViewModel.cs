using System;
using System.ComponentModel;
using JunkyardDogs.Data;
using PandeaGames.ViewModels;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

namespace JunkyardDogs.scripts.Runtime.Dialogs
{
    public class EventDialogViewModel : AbstractDialogViewModel<EventDialogViewModel>
    {
        public event Action<TournamentState.TournamentStatus> OnPlayTournament;
        
        public TournamentStaticDataReference TournamentReference;

        public void PlayTournament(TournamentState.TournamentStatus status)
        {
            if (OnPlayTournament != null)
            {
                OnPlayTournament(status);
            }
        }
    }
}