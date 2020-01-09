using JunkyardDogs.Data;
using JunkyardDogs.Views;
using PandeaGames;
using PandeaGames.Views;
using PandeaGames.Views.ViewControllers;

namespace JunkyardDogs
{
    public class JunkyardViewController: AbstractViewController
    {
        private JunkyardViewModel _junkyardViewModel;
        
        public override void Initialize(IViewController parent)
        {
            base.Initialize(parent);
            
            _junkyardViewModel = Game.Instance.GetViewModel<JunkyardViewModel>(0);
            
            _junkyardViewModel.SetJunkyard(JunkyardService.Instance.GetJunkyard(
                Game.Instance.GetStaticDataPovider<GameStaticDataProvider>().GameDataStaticData.JunkyardTestData),
                Game.Instance.GetStaticDataPovider<GameStaticDataProvider>().GameDataStaticData.JunkyardTestConfigData,
                Game.Instance.GetService<JunkyardUserService>().User);
        }

        protected override IView CreateView()
        {
            return new Views.JunkyardView();
        }
    }
}