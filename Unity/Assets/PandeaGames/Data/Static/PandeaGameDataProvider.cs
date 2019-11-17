using System;
using UnityEngine;

namespace PandeaGames.Data.Static
{
    public class PandeaGameDataProvider : AbstractStaticDataProvider <PandeaGameDataProvider>
    {
        private static readonly string PandeaGameConfigurationDataPath = "PandeaGames/PangeaGameConfiguration";
        
        private PandeaGameConfigurationData _pandeaGameConfigurationData;

        public PandeaGameConfigurationData PandeaGameConfigurationData
        {
            get { return _pandeaGameConfigurationData; }
        }
        
        protected override void InternalLoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadFailed)
        {
            try
            {
                _pandeaGameConfigurationData = Resources.Load<PandeaGameConfigurationData>(PandeaGameConfigurationDataPath);
                _pandeaGameConfigurationData.LoadAsync(onLoadSuccess, onLoadFailed);
            }
            catch (Exception e)
            {
                onLoadFailed(new LoadException("Failed to load game data.", e));
                throw;
            }
        }
    }
}