using System.ComponentModel;
using PandeaGames.Data.WeakReferences;
using PandeaGames.ViewModels;

namespace JunkyardDogs.scripts.Runtime.Dialogs
{
    public class EventDialogViewModel : AbstractDialogViewModel<EventDialogViewModel>
    {
        public struct Data
        {
            public readonly WeakReference TournamentReference;
            public readonly JunkyardUser User;

            public Data(WeakReference tournamentReference, JunkyardUser user)
            {
                TournamentReference = tournamentReference;
                User = user;
            }
        }

        public Data ModelData;

        public void SetData(Data modelData)
        {
            ModelData = modelData;
        }
    }
}