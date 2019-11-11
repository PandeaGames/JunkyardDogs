using System;
using System.Collections.Generic;
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
            NationalityDataProvider dataProvider = Game.Instance.GetStaticDataPovider<NationalityDataProvider>();
            JunkyardUser user = Game.Instance.GetService<JunkyardUserService>().User;

            var dataList = new List<NationalityStaticDataReference>();

            foreach (var refernce in dataProvider)
            {
                dataList.Add(refernce);
            }
            
            ChooseNationalityViewModel.Data data = new ChooseNationalityViewModel.Data(
                dataList,
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

        private void OnChooseNation(NationalityStaticDataReference reference)
        {
            JunkyardUser user = Game.Instance.GetService<JunkyardUserService>().User;
            user.Competitor.Nationality = reference;
            user.Consume(reference.Data.gameStartInventory.Data.GetLoot(), 0);

            if (OnNationChosen != null)
            {
                OnNationChosen();
            }
        }
    }
}