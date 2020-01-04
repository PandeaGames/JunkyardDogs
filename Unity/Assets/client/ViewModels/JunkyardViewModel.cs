using System;
using JunkyardDogs.Data;
using PandeaGames.ViewModels;

namespace JunkyardDogs
{
    public class JunkyardViewModel : AbstractViewModel
    {
        private enum JunkAreaState
        {
            Cleared,
            AvailableToCollect,
            Visible,
            Hidden
        }
        
        public event Action<ILoot[]> OnTakeJunk;
        
        public JunkyardUser User;
        private Junkyard _junkyard;

        public Junkyard junkyard
        {
            get { return _junkyard; }
        }
        

        public FogDataModel Fog;
        public InteractibleDataModel Interactible;

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

        public void SetJunkyard(Junkyard junkyard, JunkyardConfig config)
        {
            _junkyard = junkyard;
            Fog = new FogDataModel(junkyard, config);
            Interactible = new InteractibleDataModel(Fog, config);
        }
    }
}