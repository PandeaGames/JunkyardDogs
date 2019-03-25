using JunkyardDogs.Data;
using PandeaGames.Data.WeakReferences;
using PandeaGames.ViewModels;

namespace JunkyardDogs.scripts.Runtime.Dialogs
{
    public class ChooseActionDialogViewModel : AbstractDialogViewModel<ChooseActionDialogViewModel>
    {
        public WeakReference ActionList;
        public ActionStaticDataReference Selection;
    }
}