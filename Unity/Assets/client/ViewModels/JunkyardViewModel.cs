using System;
using JunkyardDogs.Data;
using PandeaGames.ViewModels;
using UnityEngine;

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
        
        public event Action<ILoot[], Vector3> OnTakeJunk;
        
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
        public SpecialChanceDataModel SpecialChanceDataModel;
        public ClearedDataModel ClearedDataModel;
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

            OnTakeJunk?.Invoke(loot, Vector3.zero);
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
            SpecialChanceDataModel = new SpecialChanceDataModel(junkyard);
            ClearedDataModel = new ClearedDataModel(junkyard);
            _width = junkyard.Width;
            _height = junkyard.Height;
        }

        public void ClearSpace(int x, int y, Vector3 worldSpace)
        {
            ClearSpace(new INTVector(x, y), worldSpace);            
        }

        public void ClearSpace(INTVector vector, Vector3 worldSpace)
        {
            LootDataModel lootDataModel = new LootDataModel(User, Time.frameCount);
            ILoot[] loot = _junkyard.Rewards[Math.Min(_junkyard.Rewards.Length - 1, Thresholds[vector])].crate.Data.GetLoot(lootDataModel);

            if (SpecialChanceDataModel[vector])
            {
                OnTakeJunk?.Invoke(loot, worldSpace);
            }
            
            _junkyard.SetCleared(vector.X, vector.Y, true);
            Fog.UpdateData(vector);
        }
    }
}