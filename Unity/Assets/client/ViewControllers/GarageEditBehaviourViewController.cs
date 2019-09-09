using JunkyardDogs.Components;
using JunkyardDogs.Data;
using JunkyardDogs.scripts.Runtime.Dialogs;
using JunkyardDogs.Views;
using PandeaGames;
using PandeaGames.Views;
using PandeaGames.Views.ViewControllers;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

namespace JunkyardDogs
{
    public class GarageEditBehaviourViewController : AbstractViewController
    {
        
        private EditBotBehaviourViewModel _vm;
        
        protected override IView CreateView()
        {
            return new GarageEditBehaviourView();
        }
        
        public override void Initialize(IViewController parent)
        {
            base.Initialize(parent);
            
            _vm = Game.Instance.GetViewModel<EditBotBehaviourViewModel>(0);
            
        }

        
    }
}