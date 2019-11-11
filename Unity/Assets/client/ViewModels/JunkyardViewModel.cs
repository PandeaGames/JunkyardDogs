using System;
using JunkyardDogs.Components;
using JunkyardDogs.Data;
using JunkyardDogs.Specifications;
using PandeaGames.ViewModels;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

namespace JunkyardDogs
{
    public class JunkyardViewModel : AbstractViewModel
    {
        public event Action<ILoot[]> OnTakeJunk;
        
        public JunkyardUser User;
        

        public void TakeJunk(LootCrateStaticDataReference lootCrate)
        {
            LootDataModel dataModel = new LootDataModel(User, 0);
            ILoot[]  loot = lootCrate.Data.GetLoot(dataModel);

            if (loot.Length == 0)
            {
                throw new IndexOutOfRangeException("Crate did not have any contents");
            }

            OnTakeJunk(loot);
        }
    }
}