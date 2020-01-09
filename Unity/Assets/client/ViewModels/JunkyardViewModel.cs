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

        private JunkyardConfig _config;

        public JunkyardConfig Config
        {
            get => _config;
        }

        public FogDataModel Fog;
        public VisibleDataModel VisibleDataModel;
        public InteractibleDataModel Interactible;
        public JunkyardThresholdDataModel Thresholds;
        private int _width;
        private int _height;

        public int Width
        {
            get { return _width; }
        }
    
        public int Height
        {
            get { return _height; }
        }
        
        public void TakeJunk(LootCrateStaticDataReference lootCrate)
        {
            LootDataModel dataModel = new LootDataModel(User, 0);
            ILoot[]  loot = lootCrate.Data.GetLoot(dataModel);

            if (loot.Length == 0)
            {
                throw new IndexOutOfRangeException("Crate did not have any contents");
            }

            OnTakeJunk?.Invoke(loot);
        }

        public void SetJunkyard(Junkyard junkyard, JunkyardConfig config, JunkyardUser User)
        {
            this.User = User;
            _config = config;
            _junkyard = junkyard;
            Fog = new FogDataModel(junkyard, config);
            Interactible = new InteractibleDataModel(Fog, config);
            VisibleDataModel = new VisibleDataModel(Fog, config);
            Thresholds = new JunkyardThresholdDataModel(junkyard, config);
            _width = junkyard.Width;
            _height = junkyard.Height;
        }

        public void ClearSpace(int x, int y)
        {
            ClearSpace(new INTVector(x, y));            
        }

        public void ClearSpace(INTVector vector)
        {
            LootDataModel lootDataModel = new LootDataModel(User, UnityEngine.Time.frameCount);
            ILoot[] loot = _junkyard.Rewards[Math.Min(_junkyard.Rewards.Length - 1, Thresholds[vector])].crate.Data.GetLoot(lootDataModel);
            OnTakeJunk?.Invoke(loot);
            _junkyard.SetCleared(vector.X, vector.Y, true);
            Fog.UpdateData(vector);
        }
    }
}