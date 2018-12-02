using System;
using JunkyardDogs.Data;
using JunkyardDogs.Views;
using PandeaGames;
using PandeaGames.Views.ViewControllers;
using PandeaGames.Views;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

namespace JunkyardDogs
{
    public class ChooseNationalityViewController : AbstractViewController
    {
        public event Action OnNationChosen;
        
        protected override void OnBeforeShow()
        {
            GameStaticDataProvider dataProvider = Game.Instance.GetStaticDataPovider<GameStaticDataProvider>();
            JunkyardUser user = Game.Instance.GetService<JunkyardUserService>().User;
            
            ChooseNationalityViewModel.Data data = new ChooseNationalityViewModel.Data(
                dataProvider.GameDataStaticData.Nations,
                user
                );

            var vm = Game.Instance.GetViewModel<ChooseNationalityViewModel>(0);
            
            vm.SetData(data);
            vm.OnNationChanged += OnChooseNation;
        }

        protected override IView CreateView()
        {
            return new ChooseNationlaityView();
        }

        private void OnChooseNation(WeakReference reference)
        {
            Game.Instance.GetService<JunkyardUserService>().User.Competitor.Nationality = reference;

            if (OnNationChosen != null)
            {
                OnNationChosen();
            }
        }
    }
}