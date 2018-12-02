using JunkyardDogs.Data;
using JunkyardDogs.Views;
using PandeaGames;
using PandeaGames.Views;
using PandeaGames.Views.ViewControllers;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

namespace JunkyardDogs
{
    public class GarageEditBehaviourViewController : AbstractViewController
    {
        protected override IView CreateView()
        {
            return new GarageEditBehaviourView();
        }
        
        protected override void OnBeforeShow()
        {
            base.OnBeforeShow();

            EditBotBehaviourViewModel vm = Game.Instance.GetViewModel<EditBotBehaviourViewModel>(0);
            vm.ActionList = Game.Instance.GetStaticDataPovider<GameStaticDataProvider>().GameDataStaticData.ActionListRef;
        }
    }
}